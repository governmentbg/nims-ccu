using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Eumis.Blob.Host.Controllers;
using Eumis.Common.Crypto;
using Eumis.Common.Db;
using NLog;

namespace Eumis.Blob.Host.Api
{
    public class BlobWriter : IDisposable
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private static readonly Sequence BlobContentSequence = new Sequence("BlobContentSequence", "DbContext");

        private SqlConnection blobDbConnection;
        private SqlConnection mainDbConnection;

        private long blobContentId;
        private int partitionId;
        private DateTime createDate;
        private Stream stream;
        private SHA256 sha256;

        public BlobWriter(SqlConnection blobDbConnection, SqlConnection mainDbConnection)
        {
            this.blobDbConnection = blobDbConnection;
            this.mainDbConnection = mainDbConnection;
        }

        public Stream OpenStream()
        {
            this.blobContentId = BlobWriter.BlobContentSequence.NextValue();
            this.partitionId = BlobPartitionFactory.NextValue();
            this.createDate = DateTime.Now;

            using (SqlCommand cmdInsert = this.CreateInsertCmd())
            {
                try
                {
                    cmdInsert.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    Logger.Error(
                        "Failed: INSERT INTO BlobContents (BlobContentId, PartitionId, [Hash], [Size], [Content], IsDeleted, CreateDate, DeleteDate) " +
                        $"VALUES ({this.blobContentId}, {this.partitionId}, NULL, NULL, NULL, 1, '{this.createDate}', '{this.createDate}');");
                    throw;
                }

                return this.CreateStream();
            }
        }

        public async Task<Stream> OpenStreamAsync()
        {
            this.blobContentId = BlobWriter.BlobContentSequence.NextValue();
            this.partitionId = BlobPartitionFactory.NextValue();
            this.createDate = DateTime.Now;

            using (SqlCommand cmdInsert = this.CreateInsertCmd())
            {
                try
                {
                    await cmdInsert.ExecuteNonQueryAsync();
                }
                catch (Exception)
                {
                    Logger.Error(
                        "Failed: INSERT INTO BlobContents (BlobContentId, PartitionId, [Hash], [Size], [Content], IsDeleted, CreateDate, DeleteDate) " +
                        $"VALUES ({this.blobContentId}, {this.partitionId}, NULL, NULL, NULL, 1, '{this.createDate}', '{this.createDate}');");
                    throw;
                }

                return this.CreateStream();
            }
        }

        public Task<BlobInfo> GetBlobInfoAsync()
        {
            // make sure noone writes to the blob after we calculate its hash
            this.stream.Close();

            return this.GetBlobInfoInternalAsync();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            try
            {
                if (disposing && this.sha256 != null && this.stream != null)
                {
                    using (this.sha256)
                    using (this.stream)
                    {
                    }
                }
            }
            finally
            {
                this.sha256 = null;
                this.stream = null;

                // we are not managing the connection so we are not disposing it
                this.blobDbConnection = null;
            }
        }

        private async Task<BlobInfo> GetBlobInfoInternalAsync()
        {
            string hash = CryptoUtils.GetHexString(this.sha256.Hash);

            var getSizeCmd = this.blobDbConnection.CreateCommand();
            getSizeCmd.CommandText = "SELECT DATALENGTH([Content]) FROM BlobContents WHERE BlobContentId = @blobContentId AND PartitionId = @partitionId AND IsDeleted = 1";
            getSizeCmd.Parameters.AddWithValue("@blobContentId", this.blobContentId);
            getSizeCmd.Parameters.AddWithValue("@partitionId", this.partitionId);

            var getSizeCmdRes = await getSizeCmd.ExecuteScalarAsync();
            long size = 0;
            if (getSizeCmdRes != DBNull.Value)
            {
                size = (long)getSizeCmdRes;
            }

            var getLocationCmd = this.mainDbConnection.CreateCommand();
            getLocationCmd.CommandText = "SELECT BlobContentLocationId, IsDeleted FROM BlobContentLocations WHERE [Hash] = @hash AND [Size] = @size";
            getLocationCmd.Parameters.AddWithValue("@hash", hash);
            getLocationCmd.Parameters.AddWithValue("@size", size);

            using (var reader = await getLocationCmd.ExecuteReaderAsync())
            {
                if (reader.HasRows)
                {
                    await reader.ReadAsync();

                    bool isDeleted = reader.GetBoolean(reader.GetOrdinal("IsDeleted"));

                    if (isDeleted)
                    {
                        var resurrectLocationCmd = this.mainDbConnection.CreateCommand();
                        resurrectLocationCmd.CommandText =
                            @"UPDATE BlobContentLocations SET
                            IsDeleted = 0,
                            DeleteDate = NULL
                          WHERE
                            [Hash] = @hash AND [Size] = @size";
                        resurrectLocationCmd.Parameters.AddWithValue("@hash", hash);
                        resurrectLocationCmd.Parameters.AddWithValue("@size", size);

                        await resurrectLocationCmd.ExecuteNonQueryAsync();
                    }

                    return new BlobInfo(reader.GetInt64(reader.GetOrdinal("BlobContentLocationId")), size, hash);
                }
            }

            var mainDbTransaction = this.mainDbConnection.BeginTransaction();
            try
            {
                var insertLocationCmd = this.mainDbConnection.CreateCommand();
                insertLocationCmd.Transaction = mainDbTransaction;
                insertLocationCmd.CommandText =
                    @"INSERT INTO BlobContentLocations (BlobContentId, PartitionId, ContentDbCSName, [Hash], [Size], IsDeleted, CreateDate, DeleteDate) 
                      VALUES (@blobContentId, @partitionId, @contentDbCSName, @hash, @size, 0, @createDate, NULL);

                      SET @blobContentLocationId = SCOPE_IDENTITY();";
                insertLocationCmd.Parameters.AddWithValue("@blobContentId", this.blobContentId);
                insertLocationCmd.Parameters.AddWithValue("@partitionId", this.partitionId);
                insertLocationCmd.Parameters.AddWithValue("@contentDbCSName", BlobsController.CurrentBlobDbConnectionStringName);
                insertLocationCmd.Parameters.AddWithValue("@hash", hash);
                insertLocationCmd.Parameters.AddWithValue("@size", size);
                insertLocationCmd.Parameters.AddWithValue("@createDate", this.createDate);
                SqlParameter blobContentLocationIdParam = new SqlParameter("@blobContentLocationId", SqlDbType.BigInt) { Direction = ParameterDirection.Output };
                insertLocationCmd.Parameters.Add(blobContentLocationIdParam);

                bool uniqueConstraintViolated = false;
                try
                {
                    await insertLocationCmd.ExecuteNonQueryAsync();
                }
                catch (SqlException sqlExc)
                {
                    if (sqlExc.Errors.Cast<SqlError>().Any(e => e.Number == 2601 && e.Message.Contains("UQ_BlobContentLocations_Hash_Size")))
                    {
                        uniqueConstraintViolated = true;
                    }
                    else
                    {
                        // not unique constraint violated
                        throw;
                    }
                }

                BlobInfo result;

                // someone uploaded this blob first
                if (uniqueConstraintViolated)
                {
                    // get the location of the uploaded blob
                    getLocationCmd.Transaction = mainDbTransaction;
                    result = new BlobInfo((long)await getLocationCmd.ExecuteScalarAsync(), size, hash);
                }

                // we succeeded in uploading the blob
                else
                {
                    // set its hash and size in the BlobContents table
                    var updateBlobCmd = this.blobDbConnection.CreateCommand();
                    updateBlobCmd.CommandText = "UPDATE BlobContents SET [Hash] = @hash, [Size] = @size, [IsDeleted] = 0, [DeleteDate] = NULL WHERE BlobContentId = @blobContentId AND PartitionId = @partitionId AND IsDeleted = 1;";
                    updateBlobCmd.Parameters.AddWithValue("@blobContentId", this.blobContentId);
                    updateBlobCmd.Parameters.AddWithValue("@partitionId", this.partitionId);
                    updateBlobCmd.Parameters.AddWithValue("@hash", hash);
                    updateBlobCmd.Parameters.AddWithValue("@size", size);

                    await updateBlobCmd.ExecuteNonQueryAsync();

                    result = new BlobInfo((long)blobContentLocationIdParam.Value, size, hash);
                }

                mainDbTransaction.Commit();

                return result;
            }
            catch
            {
                mainDbTransaction.Rollback();

                throw;
            }
        }

        private Stream CreateStream()
        {
            BlobWriteStream blobStream = new BlobWriteStream(this.blobDbConnection, null, this.blobContentId, this.partitionId);

            this.sha256 = new SHA256Managed();
            this.stream = new CryptoStream(blobStream, this.sha256, CryptoStreamMode.Write);

            return this.stream;
        }

        private SqlCommand CreateInsertCmd()
        {
            var cmd = this.blobDbConnection.CreateCommand();
            cmd.CommandText =
                @"INSERT INTO BlobContents (BlobContentId, PartitionId, [Hash], [Size], [Content], IsDeleted, CreateDate, DeleteDate) 
                    VALUES (@id, @partitionId, NULL, NULL, NULL, 1, @createDate, @createDate);";
            cmd.Parameters.AddWithValue("@id", this.blobContentId);
            cmd.Parameters.AddWithValue("@partitionId", this.partitionId);
            cmd.Parameters.AddWithValue("@createDate", this.createDate);

            return cmd;
        }
    }
}
