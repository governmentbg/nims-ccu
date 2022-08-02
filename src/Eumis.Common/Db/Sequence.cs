using Eumis.Common.Config;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Eumis.Common.Db
{
    public class Sequence
    {
        private const long RangeSize = 100;

        private object syncRoot = new object();

        private string sequenceName;
        private string connectionStringName;
        private long lastValue;
        private long incrementBy;
        private long rangeFirstValue;
        private long rangeLastValue;

        public Sequence(string sequenceName, string connectionStringName)
        {
            this.sequenceName = sequenceName;
            this.connectionStringName = connectionStringName;
            this.lastValue = 0;
            this.incrementBy = 0;
            this.rangeFirstValue = 0;
            this.rangeLastValue = 0;
        }

        public long NextValue()
        {
            lock (this.syncRoot)
            {
                if (this.lastValue == this.rangeLastValue)
                {
                    this.GetNextRange();
                    this.lastValue = this.rangeFirstValue;
                }
                else
                {
                    this.lastValue += this.incrementBy;
                }

                return this.lastValue;
            }
        }

        public int NextIntValue()
        {
            return (int)this.NextValue();
        }

        private void GetNextRange()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings[this.connectionStringName].ConnectionString.ExpandEnv()))
            {
                connection.Open();

                using (SqlCommand getRangeCmd = this.GetRangeCmd(connection, out var rangeFirstVal, out var rangeLastVal, out var sequenceIncrement))
                {
                    getRangeCmd.ExecuteNonQuery();

                    this.rangeFirstValue = (long)rangeFirstVal.Value;
                    this.rangeLastValue = (long)rangeLastVal.Value;
                    this.incrementBy = (long)sequenceIncrement.Value;
                }
            }
        }

        private SqlCommand GetRangeCmd(
            SqlConnection connection,
            out SqlParameter rangeFirstVal,
            out SqlParameter rangeLastVal,
            out SqlParameter sequenceIncrement)
        {
            var cmd = new SqlCommand("sp_sequence_get_range", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@sequence_name", this.sequenceName);
            cmd.Parameters.AddWithValue("@range_size", RangeSize);

            rangeFirstVal = new SqlParameter("@range_first_value", SqlDbType.Variant);
            rangeFirstVal.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(rangeFirstVal);

            rangeLastVal = new SqlParameter("@range_last_value", SqlDbType.Variant);
            rangeLastVal.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(rangeLastVal);

            sequenceIncrement = new SqlParameter("@sequence_increment", SqlDbType.Variant);
            sequenceIncrement.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(sequenceIncrement);

            return cmd;
        }
    }
}
