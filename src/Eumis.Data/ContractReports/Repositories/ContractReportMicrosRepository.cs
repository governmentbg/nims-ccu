using Eumis.Common.Db;
using Eumis.Common.Json;
using Eumis.Data.ContractReports.PortalViewObjects;
using Eumis.Data.Core;
using Eumis.Data.Linq;
using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.ContractReportMicros;
using Eumis.Domain.Contracts.ViewObjects;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Eumis.Data.ContractReports.Repositories
{
    internal class ContractReportMicrosRepository : AggregateRepository<ContractReportMicro>, IContractReportMicrosRepository
    {
        public ContractReportMicrosRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IList<ContractReportMicro> FindAll(int contractReportId)
        {
            return this.Set()
                .Where(p => p.ContractReportId == contractReportId)
                .ToList();
        }

        public async Task<IList<ContractReportMicro>> FindAllAsync(int contractReportId, CancellationToken ct)
        {
            return await this.Set()
                .Where(p => p.ContractReportId == contractReportId)
                .ToListAsync(ct);
        }

        public ContractReportMicro Find(Guid gid)
        {
            return this.Set()
                .Where(p => p.Gid == gid)
                .Single();
        }

        public ContractReportMicro FindForUpdate(Guid gid, byte[] version)
        {
            var micro = this.Find(gid);

            this.CheckVersion(micro.Version, version);

            return micro;
        }

        public ContractReportMicro GetActualContractReportMicro(int contractReportId, ContractReportMicroType type)
        {
            return this.Set()
                .Where(p => p.ContractReportId == contractReportId && p.Status == ContractReportMicroStatus.Actual && p.Type == type)
                .SingleOrDefault();
        }

        public ContractReportMicroType GetMicroType(Guid gid)
        {
            return (from m in this.unitOfWork.DbContext.Set<ContractReportMicro>()
                    where m.Gid == gid
                    select m.Type).Single();
        }

        public ContractReportMicroType GetMicroType(int microId)
        {
            return (from m in this.unitOfWork.DbContext.Set<ContractReportMicro>()
                    where m.ContractReportMicroId == microId
                    select m.Type).Single();
        }

        public IList<ContractReportMicroVO> GetContractReportMicros(int contractReportId)
        {
            return (from m in this.unitOfWork.DbContext.Set<ContractReportMicro>()
                    where m.ContractReportId == contractReportId
                    join creg in this.unitOfWork.DbContext.Set<ContractRegistration>() on m.SenderContractRegistrationId equals creg.ContractRegistrationId into cregs
                    from creg in cregs.DefaultIfEmpty()
                    orderby m.CreateDate descending
                    select new ContractReportMicroVO
                    {
                        ContractReportMicroId = m.ContractReportMicroId,
                        ContractReportId = m.ContractReportId,
                        ContractId = m.ContractId,
                        VersionNum = m.VersionNum,
                        VersionSubNum = m.VersionSubNum,
                        Type = m.Type,
                        Status = m.Status,
                        StatusName = m.Status,
                        StatusNote = m.StatusNote,
                        Source = m.Source,
                        SourceName = m.Source,
                        IsFromExternalSystem = m.IsFromExternalSystem,
                        CreateDate = m.CreateDate,
                        ContractRegistrationEmail = creg.Email,
                    }).ToList();
        }

        public ContractReportMicroItemsPVO<ContractReportMicroType1ItemPVO> GetPortalType1Items(Guid gid, int offset = 0, int? limit = null)
        {
            var numberData = (from rm in this.unitOfWork.DbContext.Set<ContractReportMicro>()
                              join c in this.unitOfWork.DbContext.Set<Contract>() on rm.ContractId equals c.ContractId
                              where rm.Gid == gid
                              select new { ContractNumber = c.RegNumber, VersionNum = rm.VersionNum }).Single();

            var query =
                from mi in this.unitOfWork.DbContext.Set<ContractReportMicrosType1Item>()
                join rm in this.unitOfWork.DbContext.Set<ContractReportMicro>() on mi.ContractReportMicroId equals rm.ContractReportMicroId
                join d in this.unitOfWork.DbContext.Set<ContractReportMicrosDistrict>() on mi.DistrictId equals d.ContractReportMicrosDistrictId into g0
                from d in g0.DefaultIfEmpty()
                join m in this.unitOfWork.DbContext.Set<ContractReportMicrosMunicipality>() on mi.MunicipalityId equals m.ContractReportMicrosMunicipalityId into g1
                from m in g1.DefaultIfEmpty()
                where rm.Gid == gid
                orderby mi.ContractReportMicrosType1ItemId
                select new ContractReportMicroType1ItemPVO
                {
                    District = d.Name,
                    Municipality = m.Name,
                    TotalCount = mi.TotalCount,
                    ChildrensCount = mi.ChildrensCount,
                    SeniorsCount = mi.SeniorsCount,
                    FemalesCount = mi.FemalesCount,
                    EmigrantsCount = mi.EmigrantsCount,
                    ForeignCitizensCount = mi.ForeignCitizensCount,
                    MinoritiesCount = mi.MinoritiesCount,
                    GypsiesCount = mi.GypsiesCount,
                    DisabledPersonsCount = mi.DisabledPersonsCount,
                    HomelessCount = mi.HomelessCount,
                };

            return new ContractReportMicroItemsPVO<ContractReportMicroType1ItemPVO>
            {
                ContractNumber = numberData.ContractNumber,
                VersionNum = numberData.VersionNum,
                Items = new PagePVO<ContractReportMicroType1ItemPVO>
                {
                    Count = query.Count(),
                    Results = query.WithOffsetAndLimit(offset, limit).ToList(),
                },
            };
        }

        public ContractReportMicroItemsPVO<ContractReportMicroType2ItemPVO> GetPortalType2Items(Guid gid, int offset = 0, int? limit = null)
        {
            var numberData = (from rm in this.unitOfWork.DbContext.Set<ContractReportMicro>()
                              join c in this.unitOfWork.DbContext.Set<Contract>() on rm.ContractId equals c.ContractId
                              where rm.Gid == gid
                              select new { ContractNumber = c.RegNumber, VersionNum = rm.VersionNum }).Single();

            var query =
                from mi in this.unitOfWork.DbContext.Set<ContractReportMicrosType2Item>()
                join rm in this.unitOfWork.DbContext.Set<ContractReportMicro>() on mi.ContractReportMicroId equals rm.ContractReportMicroId
                join addrd in this.unitOfWork.DbContext.Set<ContractReportMicrosDistrict>() on mi.AddressDistrictId equals addrd.ContractReportMicrosDistrictId into g0
                from addrd in g0.DefaultIfEmpty()
                join addrs in this.unitOfWork.DbContext.Set<ContractReportMicrosSettlement>() on mi.AddressSettlementId equals addrs.ContractReportMicrosSettlementId into g1
                from addrs in g1.DefaultIfEmpty()
                join apd in this.unitOfWork.DbContext.Set<ContractReportMicrosDistrict>() on mi.ActivityPlaceDistrictId equals apd.ContractReportMicrosDistrictId into g2
                from apd in g2.DefaultIfEmpty()
                join aps in this.unitOfWork.DbContext.Set<ContractReportMicrosSettlement>() on mi.ActivityPlaceSettlementId equals aps.ContractReportMicrosSettlementId into g3
                from aps in g3.DefaultIfEmpty()
                where rm.Gid == gid
                orderby mi.ContractReportMicrosType2ItemId
                select new
                {
                    mi.Number,
                    mi.FirstName,
                    mi.MiddleName,
                    mi.LastName,
                    mi.Uin,
                    mi.Gender,
                    mi.Age,
                    mi.Occupation,
                    mi.Education,
                    AddressDistrict = addrd.Name,
                    AddressSettlement = addrs.Name,
                    mi.Phone,
                    mi.Email,
                    mi.IsEmigrant,
                    mi.IsForeigner,
                    mi.IsMinority,
                    mi.IsGypsy,
                    mi.IsDisabledPerson,
                    mi.IsHomeless,
                    mi.DisadvantagedPerson,
                    mi.IsLivingInUnemployedHousehold,
                    mi.IsLivingInUnemployedHouseholdWithChildren,
                    mi.IsLivingInFamilyOfOneWithChildren,
                    mi.JoiningDate,
                    mi.Activity,
                    ActivityPlaceDistrict = apd.Name,
                    ActivityPlaceSettlement = aps.Name,
                    mi.ParticipationState,
                    mi.LeavingDate,
                    mi.CancelationReason,
                    mi.LeavingState,
                };

            return new ContractReportMicroItemsPVO<ContractReportMicroType2ItemPVO>
            {
                ContractNumber = numberData.ContractNumber,
                VersionNum = numberData.VersionNum,
                Items = new PagePVO<ContractReportMicroType2ItemPVO>
                {
                    Count = query.Count(),
                    Results = query
                        .WithOffsetAndLimit(offset, limit)
                        .ToList()
                        .Select(o => new ContractReportMicroType2ItemPVO
                        {
                            Number = o.Number,
                            FirstName = o.FirstName,
                            MiddleName = o.MiddleName,
                            LastName = o.LastName,
                            Uin = o.Uin,
                            Gender = o.Gender.HasValue ? o.Gender.GetEnumDescription() : null,
                            Age = o.Age,
                            Occupation = o.Occupation.HasValue ? o.Occupation.GetEnumDescription() : null,
                            Education = o.Education.HasValue ? o.Education.GetEnumDescription() : null,
                            AddressDistrict = o.AddressDistrict,
                            AddressSettlement = o.AddressSettlement,
                            Phone = o.Phone,
                            Email = o.Email,
                            IsEmigrant = o.IsEmigrant,
                            IsForeigner = o.IsForeigner,
                            IsMinority = o.IsMinority,
                            IsGypsy = o.IsGypsy,
                            IsDisabledPerson = o.IsDisabledPerson,
                            IsHomeless = o.IsHomeless,
                            DisadvantagedPerson = o.DisadvantagedPerson,
                            IsLivingInUnemployedHousehold = o.IsLivingInUnemployedHousehold,
                            IsLivingInUnemployedHouseholdWithChildren = o.IsLivingInUnemployedHouseholdWithChildren,
                            IsLivingInFamilyOfOneWithChildren = o.IsLivingInFamilyOfOneWithChildren,
                            JoiningDate = o.JoiningDate,
                            Activity = o.Activity,
                            ActivityPlaceDistrict = o.ActivityPlaceDistrict,
                            ActivityPlaceSettlement = o.ActivityPlaceSettlement,
                            ParticipationState = o.ParticipationState.HasValue ? o.ParticipationState.GetEnumDescription() : null,
                            LeavingDate = o.LeavingDate,
                            CancelationReason = o.CancelationReason.HasValue ? o.CancelationReason.GetEnumDescription() : null,
                            LeavingState = o.LeavingState.HasValue ? o.LeavingState.GetEnumDescription() : null,
                        }).ToList(),
                },
            };
        }

        public ContractReportMicroItemsPVO<ContractReportMicroType3ItemPVO> GetPortalType3Items(Guid gid, int offset = 0, int? limit = null)
        {
            var numberData = (from rm in this.unitOfWork.DbContext.Set<ContractReportMicro>()
                              join c in this.unitOfWork.DbContext.Set<Contract>() on rm.ContractId equals c.ContractId
                              where rm.Gid == gid
                              select new { ContractNumber = c.RegNumber, VersionNum = rm.VersionNum }).Single();

            var query =
                from mi in this.unitOfWork.DbContext.Set<ContractReportMicrosType3Item>()
                join rm in this.unitOfWork.DbContext.Set<ContractReportMicro>() on mi.ContractReportMicroId equals rm.ContractReportMicroId
                join d in this.unitOfWork.DbContext.Set<ContractReportMicrosDistrict>() on mi.DistrictId equals d.ContractReportMicrosDistrictId into g0
                from d in g0.DefaultIfEmpty()
                join m in this.unitOfWork.DbContext.Set<ContractReportMicrosMunicipality>() on mi.MunicipalityId equals m.ContractReportMicrosMunicipalityId into g1
                from m in g1.DefaultIfEmpty()
                where rm.Gid == gid
                orderby mi.ContractReportMicrosType3ItemId
                select new ContractReportMicroType3ItemPVO
                {
                    District = d.Name,
                    Municipality = m.Name,
                    KetchupTargetValue = mi.Ketchup.TargetValue,
                    KetchupActualValue = mi.Ketchup.ActualValue,
                    TomatoPasteTargetValue = mi.TomatoPaste.TargetValue,
                    TomatoPasteActualValue = mi.TomatoPaste.ActualValue,
                    GreenPeasTargetValue = mi.GreenPeas.TargetValue,
                    GreenPeasActualValue = mi.GreenPeas.ActualValue,
                    HotchPotchTargetValue = mi.HotchPotch.TargetValue,
                    HotchPotchActualValue = mi.HotchPotch.ActualValue,
                    NectarTargetValue = mi.Nectar.TargetValue,
                    NectarActualValue = mi.Nectar.ActualValue,
                    CompoteTargetValue = mi.Compote.TargetValue,
                    CompoteActualValue = mi.Compote.ActualValue,
                    JamTargetValue = mi.Jam.TargetValue,
                    JamActualValue = mi.Jam.ActualValue,
                    MeatCanTargetValue = mi.MeatCan.TargetValue,
                    MeatCanActualValue = mi.MeatCan.ActualValue,
                    FishCanTargetValue = mi.FishCan.TargetValue,
                    FishCanActualValue = mi.FishCan.ActualValue,
                    WheatFlourTargetValue = mi.WheatFlour.TargetValue,
                    WheatFlourActualValue = mi.WheatFlour.ActualValue,
                    RiceTargetValue = mi.Rice.TargetValue,
                    RiceActualValue = mi.Rice.ActualValue,
                    MacaroniTargetValue = mi.Macaroni.TargetValue,
                    MacaroniActualValue = mi.Macaroni.ActualValue,
                    BulgurTargetValue = mi.Bulgur.TargetValue,
                    BulgurActualValue = mi.Bulgur.ActualValue,
                    BeansTargetValue = mi.Beans.TargetValue,
                    BeansActualValue = mi.Beans.ActualValue,
                    LentilsTargetValue = mi.Lentils.TargetValue,
                    LentilsActualValue = mi.Lentils.ActualValue,
                    BiscuitTargetValue = mi.Biscuit.TargetValue,
                    BiscuitActualValue = mi.Biscuit.ActualValue,
                    WaffleTargetValue = mi.Waffle.TargetValue,
                    WaffleActualValue = mi.Waffle.ActualValue,
                    SugarTargetValue = mi.Sugar.TargetValue,
                    SugarActualValue = mi.Sugar.ActualValue,
                    HoneyTargetValue = mi.Honey.TargetValue,
                    HoneyActualValue = mi.Honey.ActualValue,
                    OilTargetValue = mi.Oil.TargetValue,
                    OilActualValue = mi.Oil.ActualValue,
                    LokumTargetValue = mi.Lokum.TargetValue,
                    LokumActualValue = mi.Lokum.ActualValue,
                };

            return new ContractReportMicroItemsPVO<ContractReportMicroType3ItemPVO>
            {
                ContractNumber = numberData.ContractNumber,
                VersionNum = numberData.VersionNum,
                Items = new PagePVO<ContractReportMicroType3ItemPVO>
                {
                    Count = query.Count(),
                    Results = query.WithOffsetAndLimit(offset, limit).ToList(),
                },
            };
        }

        public ContractReportMicroItemsPVO<ContractReportMicroType4ItemPVO> GetPortalType4Items(Guid gid, int offset = 0, int? limit = null)
        {
            var numberData = (from rm in this.unitOfWork.DbContext.Set<ContractReportMicro>()
                              join c in this.unitOfWork.DbContext.Set<Contract>() on rm.ContractId equals c.ContractId
                              where rm.Gid == gid
                              select new { ContractNumber = c.RegNumber, VersionNum = rm.VersionNum }).Single();

            var query =
                from mi in this.unitOfWork.DbContext.Set<ContractReportMicrosType4Item>()
                join rm in this.unitOfWork.DbContext.Set<ContractReportMicro>() on mi.ContractReportMicroId equals rm.ContractReportMicroId
                join d in this.unitOfWork.DbContext.Set<ContractReportMicrosDistrict>() on mi.DistrictId equals d.ContractReportMicrosDistrictId into g0
                from d in g0.DefaultIfEmpty()
                join m in this.unitOfWork.DbContext.Set<ContractReportMicrosMunicipality>() on mi.MunicipalityId equals m.ContractReportMicrosMunicipalityId into g1
                from m in g1.DefaultIfEmpty()
                where rm.Gid == gid
                orderby mi.ContractReportMicrosType4ItemId
                select new ContractReportMicroType4ItemPVO
                {
                    District = d.Name,
                    Municipality = m.Name,
                    FruitAmounts = mi.FruitAmounts,
                    VegetableAmounts = mi.VegetableAmounts,
                    Group1TotalAmounts = mi.Group1TotalAmounts,
                    MeatAmounts = mi.MeatAmounts,
                    EggAmounts = mi.EggAmounts,
                    FishAmounts = mi.FishAmounts,
                    Group2TotalAmounts = mi.Group2TotalAmounts,
                    FlourAmounts = mi.FlourAmounts,
                    BreadAmounts = mi.BreadAmounts,
                    PotatoAmounts = mi.PotatoAmounts,
                    RiceAmounts = mi.RiceAmounts,
                    StarchProductAmounts = mi.StarchProductAmounts,
                    Group3TotalAmounts = mi.Group3TotalAmounts,
                    SugarAmounts = mi.SugarAmounts,
                    MilkProductAmounts = mi.MilkProductAmounts,
                    FatsOrOilsAmounts = mi.FatsOrOilsAmounts,
                    FastFoodAmounts = mi.FastFoodAmounts,
                    OtherFoodAmounts = mi.OtherFoodAmounts,
                    Group4TotalAmounts = mi.Group4TotalAmounts,
                    TotalDishesCount = mi.TotalDishesCount,
                    TotalPackagesCount = mi.TotalPackagesCount,
                };

            return new ContractReportMicroItemsPVO<ContractReportMicroType4ItemPVO>
            {
                ContractNumber = numberData.ContractNumber,
                VersionNum = numberData.VersionNum,
                Items = new PagePVO<ContractReportMicroType4ItemPVO>
                {
                    Count = query.Count(),
                    Results = query.WithOffsetAndLimit(offset, limit).ToList(),
                },
            };
        }

        public bool CheckMicroHasFile(Guid gid, Guid fileKey)
        {
            return (from crf in this.unitOfWork.DbContext.Set<ContractReportMicro>()
                    where crf.Gid == gid && crf.ExcelBlobKey == fileKey
                    select crf).Any();
        }

        public int GetNextVersionNum(int contractId, ContractReportMicroType type)
        {
            var lastVersionNumber = this.Set()
                .Where(t => t.ContractId == contractId && t.Type == type)
                .Max(p => (int?)p.VersionNum);

            return lastVersionNumber.HasValue ? lastVersionNumber.Value + 1 : 1;
        }

        public async Task<int> GetNextVersionNumAsync(int contractId, ContractReportMicroType type, CancellationToken ct)
        {
            var lastVersionNumber = await this.Set()
                .Where(t => t.ContractId == contractId && t.Type == type)
                .MaxAsync(p => (int?)p.VersionNum, ct);

            return lastVersionNumber.HasValue ? lastVersionNumber.Value + 1 : 1;
        }

        public int GetNextVersionSubNum(int contractReportId, ContractReportMicroType type)
        {
            var lastVersionSubNumber = this.Set()
                .Where(p => p.ContractReportId == contractReportId && p.Type == type)
                .Max(p => (int?)p.VersionSubNum);

            return lastVersionSubNumber.HasValue ? lastVersionSubNumber.Value + 1 : 1;
        }

        public async Task<int> GetNextVersionSubNumAsync(int contractReportId, ContractReportMicroType type, CancellationToken ct)
        {
            var lastVersionSubNumber = await this.Set()
                .Where(p => p.ContractReportId == contractReportId && p.Type == type)
                .MaxAsync(p => (int?)p.VersionSubNum, ct);

            return lastVersionSubNumber.HasValue ? lastVersionSubNumber.Value + 1 : 1;
        }

        public int GetContractReportId(int microId)
        {
            return (from m in this.unitOfWork.DbContext.Set<ContractReportMicro>()
                    where m.ContractReportMicroId == microId
                    select m.ContractReportId).Single();
        }

        public async Task CopyMicrodataType1ItemsAsync(int oldMicrodataId, int newMicrodataId, CancellationToken ct)
        {
            var insertSql = $@"INSERT INTO  [dbo].[ContractReportMicrosType1Items] (
                                {nameof(ContractReportMicrosType1Item.ContractReportMicroId)}
                                ,{nameof(ContractReportMicrosType1Item.DistrictId)}
                                ,{nameof(ContractReportMicrosType1Item.MunicipalityId)}
                                ,{nameof(ContractReportMicrosType1Item.TotalCount)}
                                ,{nameof(ContractReportMicrosType1Item.ChildrensCount)}
                                ,{nameof(ContractReportMicrosType1Item.SeniorsCount)}
                                ,{nameof(ContractReportMicrosType1Item.FemalesCount)}
                                ,{nameof(ContractReportMicrosType1Item.EmigrantsCount)}
                                ,{nameof(ContractReportMicrosType1Item.ForeignCitizensCount)}
                                ,{nameof(ContractReportMicrosType1Item.MinoritiesCount)}
                                ,{nameof(ContractReportMicrosType1Item.GypsiesCount)}
                                ,{nameof(ContractReportMicrosType1Item.DisabledPersonsCount)}
                                ,{nameof(ContractReportMicrosType1Item.HomelessCount)})
                            SELECT
                                @newContractReportMicroId
                                ,{nameof(ContractReportMicrosType1Item.DistrictId)}
                                ,{nameof(ContractReportMicrosType1Item.MunicipalityId)}
                                ,{nameof(ContractReportMicrosType1Item.TotalCount)}
                                ,{nameof(ContractReportMicrosType1Item.ChildrensCount)}
                                ,{nameof(ContractReportMicrosType1Item.SeniorsCount)}
                                ,{nameof(ContractReportMicrosType1Item.FemalesCount)}
                                ,{nameof(ContractReportMicrosType1Item.EmigrantsCount)}
                                ,{nameof(ContractReportMicrosType1Item.ForeignCitizensCount)}
                                ,{nameof(ContractReportMicrosType1Item.MinoritiesCount)}
                                ,{nameof(ContractReportMicrosType1Item.GypsiesCount)}
                                ,{nameof(ContractReportMicrosType1Item.DisabledPersonsCount)}
                                ,{nameof(ContractReportMicrosType1Item.HomelessCount)}
                            FROM 
                                [dbo].[ContractReportMicrosType1Items]
                            WHERE 
                                {nameof(ContractReportMicrosType1Item.ContractReportMicroId)} = @oldContractReportMicroId;";

            await this.ExecuteSqlCommandAsync(
                insertSql,
                ct,
                new SqlParameter("@newContractReportMicroId", newMicrodataId),
                new SqlParameter("@oldContractReportMicroId", oldMicrodataId));
        }

        public async Task CopyMicrodataType2ItemsAsync(int oldMicrodataId, int newMicrodataId, CancellationToken ct)
        {
            var insertSql = $@"INSERT INTO [dbo].[ContractReportMicrosType2Items] (
                                {nameof(ContractReportMicrosType2Item.ContractReportMicroId)} 
                                ,{nameof(ContractReportMicrosType2Item.Number)}
                                ,{nameof(ContractReportMicrosType2Item.FirstName)}
                                ,{nameof(ContractReportMicrosType2Item.MiddleName)}
                                ,{nameof(ContractReportMicrosType2Item.LastName)}
                                ,{nameof(ContractReportMicrosType2Item.Uin)}
                                ,{nameof(ContractReportMicrosType2Item.Gender)}
                                ,{nameof(ContractReportMicrosType2Item.Age)}
                                ,{nameof(ContractReportMicrosType2Item.Occupation)}
                                ,{nameof(ContractReportMicrosType2Item.Education)}
                                ,{nameof(ContractReportMicrosType2Item.AddressDistrictId)}
                                ,{nameof(ContractReportMicrosType2Item.AddressSettlementId)}
                                ,{nameof(ContractReportMicrosType2Item.Phone)}
                                ,{nameof(ContractReportMicrosType2Item.Email)}
                                ,{nameof(ContractReportMicrosType2Item.IsEmigrant)}
                                ,{nameof(ContractReportMicrosType2Item.IsForeigner)}
                                ,{nameof(ContractReportMicrosType2Item.IsMinority)}
                                ,{nameof(ContractReportMicrosType2Item.IsGypsy)}
                                ,{nameof(ContractReportMicrosType2Item.IsDisabledPerson)}
                                ,{nameof(ContractReportMicrosType2Item.IsHomeless)}
                                ,{nameof(ContractReportMicrosType2Item.IsLivingInUnemployedHousehold)}
                                ,{nameof(ContractReportMicrosType2Item.IsLivingInUnemployedHouseholdWithChildren)}
                                ,{nameof(ContractReportMicrosType2Item.IsLivingInFamilyOfOneWithChildren)}
                                ,{nameof(ContractReportMicrosType2Item.JoiningDate)}
                                ,{nameof(ContractReportMicrosType2Item.Activity)}
                                ,{nameof(ContractReportMicrosType2Item.ActivityPlaceDistrictId)}
                                ,{nameof(ContractReportMicrosType2Item.ActivityPlaceSettlementId)}
                                ,{nameof(ContractReportMicrosType2Item.ParticipationState)}
                                ,{nameof(ContractReportMicrosType2Item.LeavingDate)}
                                ,{nameof(ContractReportMicrosType2Item.DisadvantagedPerson)}
                                ,{nameof(ContractReportMicrosType2Item.CancelationReason)}
                                ,{nameof(ContractReportMicrosType2Item.LeavingState)}) 
                            SELECT 
                                @newContractReportMicroId
                                ,{nameof(ContractReportMicrosType2Item.Number)}
                                ,{nameof(ContractReportMicrosType2Item.FirstName)}
                                ,{nameof(ContractReportMicrosType2Item.MiddleName)}
                                ,{nameof(ContractReportMicrosType2Item.LastName)}
                                ,{nameof(ContractReportMicrosType2Item.Uin)}
                                ,{nameof(ContractReportMicrosType2Item.Gender)}
                                ,{nameof(ContractReportMicrosType2Item.Age)}
                                ,{nameof(ContractReportMicrosType2Item.Occupation)}
                                ,{nameof(ContractReportMicrosType2Item.Education)}
                                ,{nameof(ContractReportMicrosType2Item.AddressDistrictId)}
                                ,{nameof(ContractReportMicrosType2Item.AddressSettlementId)}
                                ,{nameof(ContractReportMicrosType2Item.Phone)}
                                ,{nameof(ContractReportMicrosType2Item.Email)}
                                ,{nameof(ContractReportMicrosType2Item.IsEmigrant)}
                                ,{nameof(ContractReportMicrosType2Item.IsForeigner)}
                                ,{nameof(ContractReportMicrosType2Item.IsMinority)}
                                ,{nameof(ContractReportMicrosType2Item.IsGypsy)}
                                ,{nameof(ContractReportMicrosType2Item.IsDisabledPerson)}
                                ,{nameof(ContractReportMicrosType2Item.IsHomeless)}
                                ,{nameof(ContractReportMicrosType2Item.IsLivingInUnemployedHousehold)}
                                ,{nameof(ContractReportMicrosType2Item.IsLivingInUnemployedHouseholdWithChildren)}
                                ,{nameof(ContractReportMicrosType2Item.IsLivingInFamilyOfOneWithChildren)}
                                ,{nameof(ContractReportMicrosType2Item.JoiningDate)}
                                ,{nameof(ContractReportMicrosType2Item.Activity)}
                                ,{nameof(ContractReportMicrosType2Item.ActivityPlaceDistrictId)}
                                ,{nameof(ContractReportMicrosType2Item.ActivityPlaceSettlementId)}
                                ,{nameof(ContractReportMicrosType2Item.ParticipationState)}
                                ,{nameof(ContractReportMicrosType2Item.LeavingDate)}
                                ,{nameof(ContractReportMicrosType2Item.DisadvantagedPerson)}
                                ,{nameof(ContractReportMicrosType2Item.CancelationReason)}
                                ,{nameof(ContractReportMicrosType2Item.LeavingState)}
                            FROM 
                                [dbo].[ContractReportMicrosType2Items]  
                            WHERE 
                                {nameof(ContractReportMicrosType2Item.ContractReportMicroId)} = @oldContractReportMicroId;";

            await this.ExecuteSqlCommandAsync(
                insertSql,
                ct,
                new SqlParameter("@newContractReportMicroId", newMicrodataId),
                new SqlParameter("@oldContractReportMicroId", oldMicrodataId));
        }

        public async Task CopyMicrodataType3ItemsAsync(int oldMicrodataId, int newMicrodataId, CancellationToken ct)
        {
            var insertSql = $@"INSERT INTO [dbo].[ContractReportMicrosType3Items] (
                                {nameof(ContractReportMicrosType3Item.ContractReportMicroId)}
                                ,{nameof(ContractReportMicrosType3Item.DistrictId)}
                                ,{nameof(ContractReportMicrosType3Item.MunicipalityId)}
                                ,[KetchupTargetValue]
                                ,[KetchupActualValue]
                                ,[TomatoPasteTargetValue]
                                ,[TomatoPasteActualValue]
                                ,[GreenPeasTargetValue]
                                ,[GreenPeasActualValue]
                                ,[HotchPotchTargetValue]
                                ,[HotchPotchActualValue]
                                ,[NectarTargetValue]
                                ,[NectarActualValue]
                                ,[CompoteTargetValue]
                                ,[CompoteActualValue]
                                ,[JamTargetValue]
                                ,[JamActualValue]
                                ,[MeatCanTargetValue]
                                ,[MeatCanActualValue]
                                ,[FishCanTargetValue]
                                ,[FishCanActualValue]
                                ,[WheatFlourTargetValue]
                                ,[WheatFlourActualValue]
                                ,[RiceTargetValue]
                                ,[RiceActualValue]
                                ,[MacaroniTargetValue]
                                ,[MacaroniActualValue]
                                ,[BulgurTargetValue]
                                ,[BulgurActualValue]
                                ,[BeansTargetValue]
                                ,[BeansActualValue]
                                ,[LentilsTargetValue]
                                ,[LentilsActualValue]
                                ,[BiscuitTargetValue]
                                ,[BiscuitActualValue]
                                ,[WaffleTargetValue]
                                ,[WaffleActualValue]
                                ,[SugarTargetValue]
                                ,[SugarActualValue]
                                ,[HoneyTargetValue]
                                ,[HoneyActualValue]
                                ,[OilTargetValue]
                                ,[OilActualValue]
                                ,[LokumTargetValue]
                                ,[LokumActualValue])
                            SELECT
                                @newContractReportMicroId
                                ,{nameof(ContractReportMicrosType3Item.DistrictId)}
                                ,{nameof(ContractReportMicrosType3Item.MunicipalityId)}
                                ,[KetchupTargetValue]
                                ,[KetchupActualValue]
                                ,[TomatoPasteTargetValue]
                                ,[TomatoPasteActualValue]
                                ,[GreenPeasTargetValue]
                                ,[GreenPeasActualValue]
                                ,[HotchPotchTargetValue]
                                ,[HotchPotchActualValue]
                                ,[NectarTargetValue]
                                ,[NectarActualValue]
                                ,[CompoteTargetValue]
                                ,[CompoteActualValue]
                                ,[JamTargetValue]
                                ,[JamActualValue]
                                ,[MeatCanTargetValue]
                                ,[MeatCanActualValue]
                                ,[FishCanTargetValue]
                                ,[FishCanActualValue]
                                ,[WheatFlourTargetValue]
                                ,[WheatFlourActualValue]
                                ,[RiceTargetValue]
                                ,[RiceActualValue]
                                ,[MacaroniTargetValue]
                                ,[MacaroniActualValue]
                                ,[BulgurTargetValue]
                                ,[BulgurActualValue]
                                ,[BeansTargetValue]
                                ,[BeansActualValue]
                                ,[LentilsTargetValue]
                                ,[LentilsActualValue]
                                ,[BiscuitTargetValue]
                                ,[BiscuitActualValue]
                                ,[WaffleTargetValue]
                                ,[WaffleActualValue]
                                ,[SugarTargetValue]
                                ,[SugarActualValue]
                                ,[HoneyTargetValue]
                                ,[HoneyActualValue]
                                ,[OilTargetValue]
                                ,[OilActualValue]
                                ,[LokumTargetValue]
                                ,[LokumActualValue]
                            FROM 
                                [dbo].[ContractReportMicrosType3Items]
                            WHERE 
                                {nameof(ContractReportMicrosType3Item.ContractReportMicroId)} = @oldContractReportMicroId;";

            await this.ExecuteSqlCommandAsync(
                insertSql,
                ct,
                new SqlParameter("@newContractReportMicroId", newMicrodataId),
                new SqlParameter("@oldContractReportMicroId", oldMicrodataId));
        }

        public async Task CopyMicrodataType4ItemsAsync(int oldMicrodataId, int newMicrodataId, CancellationToken ct)
        {
            var insertSql = $@"INSERT INTO [dbo].[ContractReportMicrosType4Items] (
                                {nameof(ContractReportMicrosType4Item.ContractReportMicroId)}
                                ,{nameof(ContractReportMicrosType4Item.DistrictId)}
                                ,{nameof(ContractReportMicrosType4Item.MunicipalityId)}
                                ,{nameof(ContractReportMicrosType4Item.FruitAmounts)}
                                ,{nameof(ContractReportMicrosType4Item.VegetableAmounts)}
                                ,{nameof(ContractReportMicrosType4Item.Group1TotalAmounts)}
                                ,{nameof(ContractReportMicrosType4Item.MeatAmounts)}
                                ,{nameof(ContractReportMicrosType4Item.EggAmounts)}
                                ,{nameof(ContractReportMicrosType4Item.FishAmounts)}
                                ,{nameof(ContractReportMicrosType4Item.Group2TotalAmounts)}
                                ,{nameof(ContractReportMicrosType4Item.FlourAmounts)}
                                ,{nameof(ContractReportMicrosType4Item.BreadAmounts)}
                                ,{nameof(ContractReportMicrosType4Item.PotatoAmounts)}
                                ,{nameof(ContractReportMicrosType4Item.RiceAmounts)}
                                ,{nameof(ContractReportMicrosType4Item.StarchProductAmounts)}
                                ,{nameof(ContractReportMicrosType4Item.Group3TotalAmounts)}
                                ,{nameof(ContractReportMicrosType4Item.SugarAmounts)}
                                ,{nameof(ContractReportMicrosType4Item.MilkProductAmounts)}
                                ,{nameof(ContractReportMicrosType4Item.FatsOrOilsAmounts)}
                                ,{nameof(ContractReportMicrosType4Item.FastFoodAmounts)}
                                ,{nameof(ContractReportMicrosType4Item.OtherFoodAmounts)}
                                ,{nameof(ContractReportMicrosType4Item.Group4TotalAmounts)}
                                ,{nameof(ContractReportMicrosType4Item.TotalDishesCount)}
                                ,{nameof(ContractReportMicrosType4Item.TotalPackagesCount)})
                            SELECT
                                @newContractReportMicroId
                                ,{nameof(ContractReportMicrosType4Item.DistrictId)}
                                ,{nameof(ContractReportMicrosType4Item.MunicipalityId)}
                                ,{nameof(ContractReportMicrosType4Item.FruitAmounts)}
                                ,{nameof(ContractReportMicrosType4Item.VegetableAmounts)}
                                ,{nameof(ContractReportMicrosType4Item.Group1TotalAmounts)}
                                ,{nameof(ContractReportMicrosType4Item.MeatAmounts)}
                                ,{nameof(ContractReportMicrosType4Item.EggAmounts)}
                                ,{nameof(ContractReportMicrosType4Item.FishAmounts)}
                                ,{nameof(ContractReportMicrosType4Item.Group2TotalAmounts)}
                                ,{nameof(ContractReportMicrosType4Item.FlourAmounts)}
                                ,{nameof(ContractReportMicrosType4Item.BreadAmounts)}
                                ,{nameof(ContractReportMicrosType4Item.PotatoAmounts)}
                                ,{nameof(ContractReportMicrosType4Item.RiceAmounts)}
                                ,{nameof(ContractReportMicrosType4Item.StarchProductAmounts)}
                                ,{nameof(ContractReportMicrosType4Item.Group3TotalAmounts)}
                                ,{nameof(ContractReportMicrosType4Item.SugarAmounts)}
                                ,{nameof(ContractReportMicrosType4Item.MilkProductAmounts)}
                                ,{nameof(ContractReportMicrosType4Item.FatsOrOilsAmounts)}
                                ,{nameof(ContractReportMicrosType4Item.FastFoodAmounts)}
                                ,{nameof(ContractReportMicrosType4Item.OtherFoodAmounts)}
                                ,{nameof(ContractReportMicrosType4Item.Group4TotalAmounts)}
                                ,{nameof(ContractReportMicrosType4Item.TotalDishesCount)}
                                ,{nameof(ContractReportMicrosType4Item.TotalPackagesCount)}
                            FROM 
                                [dbo].[ContractReportMicrosType4Items]
                            WHERE 
                                {nameof(ContractReportMicrosType4Item.ContractReportMicroId)} = @oldContractReportMicroId;";

            await this.ExecuteSqlCommandAsync(
                insertSql,
                ct,
                new SqlParameter("@newContractReportMicroId", newMicrodataId),
                new SqlParameter("@oldContractReportMicroId", oldMicrodataId));
        }
    }
}
