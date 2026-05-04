using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Polly;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Farbank.Pattern.Nester.SageSpecs.Models
{
    public class SpecsDbContext : DbContext
    {

        public SpecsDbContext(DbContextOptions<SpecsDbContext> options) : base(options)
        {
            if (this.Database.IsRelational())
            {
                this.Database.SetCommandTimeout(5600);
            }
        }

        //public DbSet<AuditTrail> AuditTrails { get; set; }
        //public DbSet<BomHeader> BomHeaders { get; set; }
        //public DbSet<BomLine> BomLines { get; set; }
        //public DbSet<BomOnHandQuantity> BomOnHandQuantities { get; set; }
        //public DbSet<BLSReport> BLSReports { get; set; }
        //public DbSet<TargetBLS> TargetBLSReport { get; set; }
        //public DbSet<InputTargetBLS> InputTargetBLSReport { get; set; }
        //public DbSet<Blank> Blanks { get; set; }
        //public DbSet<BlankPattern> BlankPatterns { get; set; }
        //public DbSet<BlankPatternDimension> BlankPatternDimensions { get; set; }
        //public DbSet<BlankView> BlanksView { get; set; }
        //public DbSet<BlankFerruleView> BlankFerrulesView { get; set; }
        //public DbSet<Mandrel> Mandrels { get; set; }
        //public DbSet<MaterialMap> Materials { get; set; }

        //public DbSet<Setting> Settings { get; set; }
        public DbSet<BlankWorkOrderException> Exceptions { get; set; }
        public DbSet<BlankWorkOrderExclusion> Exclusions { get; set; }
        //public DbSet<RodWorkOrderExclusion> RodExclusions { get; set; }
        //public DbSet<BlankWorkOrderSetting> BlankWorkOrderSettings { get; set; }
        //public DbSet<ExpediteReport> ExpediteWipReport { get; set; }
        //public DbSet<SubaStart> SubaStarts { get; set; }
        //public DbSet<RnDBlankView> RnDBlanksView { get; set; }
        //public DbSet<RnDBlankFerruleView> RnDBlankFerrulesView { get; set; }
        //public DbSet<RnDSampleRequest> RnDSampleRequests { get; set; }
        //public DbSet<RnDPattern> RnDPatterns { get; set; }
        //public DbSet<RnDDimension> RnDDimensions { get; set; }
        //public DbSet<WarrantyDemand> WarrantyDemands { get; set; }
        //public DbSet<CuttingRoomRule> CuttingRoomRules { get; set; }
        ////public List<FerruleMaterialGroup> GetFerrules(List<string> batchWorkOrders, List<FBEProductionOrderHeader> _productionOrders)
        ////{
        ////    var workOrders = new List<FBEProductionOrderHeader>();
        ////    MatchWorkOrders(batchWorkOrders, _productionOrders, workOrders);
        ////    var records = GetFerruleRecords(workOrders);

        ////    var materialGroups = records.GroupBy(g => new { g.Material, g.Graphite, g.Scrim })
        ////        .Select(x => new FerruleMaterialGroup
        ////        {
        ////            Material = x.Key.Material,
        ////            Graphite = x.Key.Graphite,
        ////            Scrim = x.Key.Scrim,
        ////            Ferrules = x.Select(f => new Ferrule
        ////            {
        ////                Bottom = f.Bottom,
        ////                Description = f.Description,
        ////                Height = f.Height,
        ////                Quantity = f.QuantityInQueue,
        ////                Right = f.Right,
        ////                Sku = f.Sku,
        ////                Top = f.Top,
        ////                WorkOrder = f.WorkOrder
        ////            }).ToList()
        ////        }).ToList();

        ////    return materialGroups;

        ////}
        ////private List<FerruleRecord> GetFerruleRecords(List<FBEProductionOrderHeader> workOrders)
        ////{
        ////    var records = new List<FerruleRecord>();
        ////    foreach (var workOrder in workOrders)
        ////    {
        ////        try
        ////        {
        ////            var ferrule = BlankFerrulesView.FirstOrDefault(f => f.Sku == workOrder.ItemId);
        ////            if (ferrule != null)
        ////            {

        ////                var graphite = string.IsNullOrWhiteSpace(ferrule.FerruleGraphite) ? "" : ferrule.FerruleGraphite.Replace("SAG ", "").Replace("-1000", "").Replace("/Hoop", "").Replace("-Hoop", "").Replace("G30-20", "G30/20").Replace("G30-75", "G30/75").Replace("G50", "G4735");
        ////                var scrim = string.IsNullOrWhiteSpace(ferrule.FerruleScrim) ? "" : ferrule.FerruleScrim.Replace("SAG ", "").Replace("-1000", "").Replace("/Hoop", "").Replace("-Hoop", "").Replace("G30-20", "G30/20").Replace("G30-75", "G30/75").Replace("G50", "G4735");
        ////                string material;

        ////                if (string.IsNullOrWhiteSpace(ferrule.FerruleGraphite))
        ////                {
        ////                    material = GetMaterialDescription("", ferrule.FerruleScrim.Replace("/Hoop", "").Replace("-Hoop", "").Replace("G30-20", "G30/20").Replace("G30-75", "G30/75").Replace("G50", "G4735"));
        ////                }
        ////                else if (string.IsNullOrWhiteSpace(ferrule.FerruleScrim))
        ////                {
        ////                    material = GetMaterialDescription(ferrule.FerruleGraphite.Replace("-1000", "").Replace("/Hoop", "").Replace("-Hoop", "").Replace("G30-20", "G30/20").Replace("G30-75", "G30/75").Replace("G50", "G4735"), "");
        ////                }
        ////                else
        ////                {
        ////                    material = GetMaterialDescription(ferrule.FerruleGraphite.Replace("-1000", "").Replace("/Hoop", "").Replace("-Hoop", "").Replace("G30-20", "G30/20").Replace("G30-75", "G30/75").Replace("G50", "G4735"), ferrule.FerruleScrim.Replace("G30-20", "G30/20").Replace("G30-75", "G30/75").Replace("G50", "G4735"));
        ////                }

        ////                var record = new FerruleRecord
        ////                {
        ////                    Material = material,
        ////                    Graphite = graphite,
        ////                    Scrim = scrim,
        ////                    WorkOrder = workOrder.ProdId,
        ////                    Sku = ferrule.Sku,
        ////                    Description = "Ferrule Description",
        ////                    QuantityInQueue = Convert.ToInt32(workOrder.QtySched),
        ////                    Bottom = Convert.ToDouble(ferrule.FerruleBottom),
        ////                    Top = Convert.ToDouble(ferrule.FerruleTop),
        ////                    Right = Convert.ToDouble(ferrule.FerruleRight),
        ////                    Height = Convert.ToDouble(ferrule.FerruleLeft),
        ////                };

        ////                records.Add(record);
        ////            }
        ////            else
        ////            {
        ////                var rndFerrule = RnDBlankFerrulesView.FirstOrDefault(f => f.WorkOrderName == workOrder.ProdId);
        ////                if (rndFerrule != null)
        ////                {

        ////                    var graphite = string.IsNullOrWhiteSpace(rndFerrule.FerruleGraphite) ? "" : rndFerrule.FerruleGraphite.Replace("SAG ", "").Replace("-1000", "").Replace("/Hoop", "").Replace("-Hoop", "").Replace("G30-20", "G30/20").Replace("G30-75", "G30/75").Replace("G50", "G4735");
        ////                    var scrim = string.IsNullOrWhiteSpace(rndFerrule.FerruleScrim) ? "" : rndFerrule.FerruleScrim.Replace("SAG ", "").Replace("-1000", "").Replace("/Hoop", "").Replace("-Hoop", "").Replace("G30-20", "G30/20").Replace("G30-75", "G30/75").Replace("G50", "G4735");
        ////                    string material;

        ////                    if (string.IsNullOrWhiteSpace(rndFerrule.FerruleGraphite))
        ////                    {
        ////                        material = GetMaterialDescription("", rndFerrule.FerruleScrim.Replace("/Hoop", "").Replace("-Hoop", "").Replace("G30-20", "G30/20").Replace("G30-75", "G30/75").Replace("G50", "G4735"));
        ////                    }
        ////                    else if (string.IsNullOrWhiteSpace(rndFerrule.FerruleScrim))
        ////                    {
        ////                        material = GetMaterialDescription(rndFerrule.FerruleGraphite.Replace("-1000", "").Replace("/Hoop", "").Replace("-Hoop", "").Replace("G30-20", "G30/20").Replace("G30-75", "G30/75").Replace("G50", "G4735"), "");
        ////                    }
        ////                    else
        ////                    {
        ////                        material = GetMaterialDescription(rndFerrule.FerruleGraphite.Replace("-1000", "").Replace("/Hoop", "").Replace("-Hoop", "").Replace("G30-20", "G30/20").Replace("G30-75", "G30/75").Replace("G50", "G4735"), rndFerrule.FerruleScrim.Replace("G30-20", "G30/20").Replace("G30-75", "G30/75").Replace("G50", "G4735"));
        ////                    }

        ////                    var record = new FerruleRecord
        ////                    {
        ////                        Material = material,
        ////                        Graphite = graphite,
        ////                        Scrim = scrim,
        ////                        WorkOrder = workOrder.ProdId,
        ////                        Sku = rndFerrule.Sku,
        ////                        Description = "Ferrule Description",
        ////                        QuantityInQueue = Convert.ToInt32(workOrder.QtySched),
        ////                        Bottom = Convert.ToDouble(rndFerrule.FerruleBottom),
        ////                        Top = Convert.ToDouble(rndFerrule.FerruleTop),
        ////                        Right = Convert.ToDouble(rndFerrule.FerruleRight),
        ////                        Height = Convert.ToDouble(rndFerrule.FerruleLeft),
        ////                    };

        ////                    records.Add(record);
        ////                }
        ////            }
        ////        }
        ////        catch (Exception e)
        ////        {
        ////            Log.Error(e.Message);
        ////        }
        ////    }

        ////    return records;
        ////}

        ////public List<MaterialGroup> GetWorkOrders(List<string> batchWorkOrders, List<FBEProductionOrderHeader> _productionOrders)
        ////{
        ////    //Potential TODO:
        ////    //maybe add the prod order headers table to byod and run the export after creating the work orders
        ////    //then add the proper view to do the whole query in d365
        ////    Log.Information("GetWorkOrders started");
        ////    var workOrders = new List<FBEProductionOrderHeader>();
        ////    MatchWorkOrders(batchWorkOrders, _productionOrders, workOrders);
        ////    var records = GetWorkOrderRecords(workOrders);
        ////    Log.Information("GetWorkOrders retrieved work order records");
        ////    Log.Information($"Work Order Count: {records.Count}");
        ////    Log.Information("Grouping work order records by material, graphite, and scrim");
        ////    var materialGroups = records.GroupBy(g => new { g.Material, g.Graphite, g.Scrim })
        ////        .Select(x => new MaterialGroup
        ////        {
        ////            Material = x.Key.Material,
        ////            Graphite = x.Key.Graphite,
        ////            Scrim = x.Key.Scrim,
        ////            Patterns = x.GroupBy(p => new { p.WorkOrder, p.Sku, p.Description, p.PatternNumber, p.QuantityInQueue })
        ////                .Select(gp => new Pattern
        ////                {
        ////                    Description = gp.Key.Description,
        ////                    Sku = gp.Key.Sku,
        ////                    PatternNumber = gp.Key.PatternNumber,
        ////                    Quantity = gp.Key.QuantityInQueue,
        ////                    WorkOrder = gp.Key.WorkOrder,
        ////                    Dimensions = gp.OrderBy(o => o.DimensionNumber).Select(d => new Dimension
        ////                    {
        ////                        DimensionNumber = d.DimensionNumber,
        ////                        Height = d.Height,
        ////                        Offset = d.Offset
        ////                    }).ToList()
        ////                }).ToList()
        ////        }).ToList();

        ////    foreach (var materialGroup in materialGroups)
        ////    {
        ////        foreach (var pattern in materialGroup.Patterns)
        ////        {
        ////            decimal offset = 0;
        ////            foreach (var dimension in ((Pattern)pattern).Dimensions.OrderBy(d => d.DimensionNumber))
        ////            {
        ////                offset += dimension.Offset;
        ////                dimension.Location = offset;
        ////            }
        ////        }
        ////    }
        ////    return materialGroups;

        ////}
        //    public async Task<BomHeader?> GetBomHeaderAsync(string sku)
        //    {
        //        var result = await BomHeaders.SingleOrDefaultAsync(b => b.ManufacturedItemNumber == sku);
        //        if (result != null)
        //        {
        //            result.BomLines = await BomLines.Where(l => l.BomHeaderId == result.BomHeaderId).ToListAsync();
        //        }

        //        return result;
        //    }
        //    private static void MatchWorkOrders(List<string> batchWorkOrders, List<FBEProductionOrderHeader> _productionOrders, List<FBEProductionOrderHeader> workOrders)
        //    {
        //        List<string> matchedWorkOrders = new();
        //        foreach (var workOrderName in batchWorkOrders)
        //        {
        //            var tmpWorkOrder = _productionOrders.FirstOrDefault(p => p.ProdId == workOrderName);
        //            Log.Warning($"Matching work order: {workOrderName}");
        //            if (!matchedWorkOrders.Contains(workOrderName) && tmpWorkOrder != null && !string.IsNullOrWhiteSpace(tmpWorkOrder.ProdId))
        //            {
        //                workOrders.Add(tmpWorkOrder);
        //                matchedWorkOrders.Add(workOrderName);
        //            }
        //            else
        //            {
        //                Log.Fatal($"Work order already matched!: {workOrderName}");
        //            }
        //        }
        //    }

        //    private List<WorkOrderRecord> GetWorkOrderRecords(List<FBEProductionOrderHeader> workOrders)
        //    {
        //        Log.Information("GetWorkOrderRecords started");
        //        var records = new List<WorkOrderRecord>();
        //        foreach (var workOrder in workOrders)
        //        {
        //            try
        //            {
        //                List<BlankView>? blanks = null;
        //                if (workOrder.ItemId != null)
        //                {
        //                    Log.Information("BlanksView select started, ItemId : " + workOrder.ItemId);
        //                    blanks = BlanksView.Where(b => b.Sku == workOrder.ItemId)?.ToList();
        //                }
        //                else
        //                {
        //                    Log.Information("BlanksView select started, ItemId is null");                        
        //                }
        //                if (blanks != null && blanks.Count > 0)
        //                {
        //                    Log.Information("BlanksView select have data");
        //                    foreach (var pattern in blanks)
        //                    {
        //                        var graphite = getPatternGraphite(pattern.Graphite);
        //                        var scrim = getPatternScrim(pattern.Scrim);
        //                        string material = getPatternMaterial(graphite, scrim);

        //                        var pat = new WorkOrderRecord
        //                        {
        //                            Material = material,
        //                            Graphite = graphite,
        //                            Scrim = scrim,
        //                            WorkOrder = workOrder.ProdId,
        //                            Sku = pattern.Sku,
        //                            Description = "Description",
        //                            QuantityInQueue = Convert.ToInt32(workOrder.QtySched),
        //                            PatternNumber = pattern.PatternNumber,
        //                            DimensionNumber = pattern.DimensionNumber,
        //                            Offset = Convert.ToDecimal(pattern.Offset),
        //                            Height = Convert.ToDecimal(pattern.Height)
        //                        };
        //                        records.Add(pat);
        //                    }
        //                }
        //                else if (blanks == null || blanks.Count == 0)
        //                {
        //                    List<RnDBlankView> rndBlanks = new List<RnDBlankView>();
        //                    if (workOrder.ProdId != null)
        //                    {
        //                        Log.Information("RnDBlanksView select started, ProdId : " + workOrder.ProdId);
        //                        rndBlanks = RnDBlanksView.Where(b => b.WorkOrderName == workOrder.ProdId).ToList();
        //                    }
        //                    else
        //                    {
        //                        Log.Information("RnDBlanksView select started, ProdId is null");
        //                    }
        //                    foreach (var pattern in rndBlanks)
        //                    {
        //                        Log.Information("RnDBlanksView select have data");
        //                        var graphite = getPatternGraphite(pattern.Graphite);
        //                        var scrim = getPatternScrim(pattern.Scrim);
        //                        string material = getPatternMaterial(graphite, scrim);

        //                        var pat = new WorkOrderRecord
        //                        {
        //                            Material = material,
        //                            Graphite = graphite,
        //                            Scrim = scrim,
        //                            WorkOrder = workOrder.ProdId,
        //                            Sku = pattern.Name,
        //                            Description = "Description",
        //                            QuantityInQueue = Convert.ToInt32(workOrder.QtySched),
        //                            PatternNumber = pattern.PatternNumber,
        //                            DimensionNumber = pattern.DimensionNumber,
        //                            Offset = Convert.ToDecimal(pattern.Offset),
        //                            Height = Convert.ToDecimal(pattern.Height)
        //                        };
        //                        records.Add(pat);
        //                    }
        //                }
        //            }
        //            catch (Exception e)
        //            {
        //                Log.Information("Error in GetWorkOrderRecords");
        //                Log.Error(e.Message);
        //            }
        //        }

        //        return records;
        //    }

        //    private string getPatternGraphite(string graphite)
        //    {
        //        return string.IsNullOrWhiteSpace(graphite) ? "" : graphite.Replace("SAG ", "").Replace("-1000", "").Replace("/Hoop", "").Replace("-Hoop", "").Replace("G30-20", "G30/20").Replace("G30-75", "G30/75").Replace("G50", "G4735");
        //    }
        //    private string getPatternScrim(string scrim)
        //    {
        //        return string.IsNullOrWhiteSpace(scrim) ? "" : scrim.Replace("SAG ", "").Replace("-1000", "").Replace("/Hoop", "").Replace("-Hoop", "").Replace("G30-20", "G30/20").Replace("G30-75", "G30/75").Replace("G50", "G4735");
        //    }
        //    private string getPatternMaterial(string graphite, string scrim)
        //    {
        //        if (string.IsNullOrWhiteSpace(graphite))
        //        {
        //            return GetMaterialDescription("", scrim.Replace("/Hoop", "").Replace("-Hoop", "").Replace("G30-20", "G30/20").Replace("G30-75", "G30/75").Replace("G50", "G4735"));
        //        }
        //        else if (string.IsNullOrWhiteSpace(scrim))
        //        {
        //            return GetMaterialDescription(graphite.Replace("-1000", "").Replace("/Hoop", "").Replace("-Hoop", "").Replace("G30-20", "G30/20").Replace("G30-75", "G30/75").Replace("G50", "G4735"), "");
        //        }
        //        else
        //        {
        //            return GetMaterialDescription(graphite.Replace("-1000", "").Replace("/Hoop", "").Replace("-Hoop", "").Replace("G30-20", "G30/20").Replace("G30-75", "G30/75").Replace("G50", "G4735"), scrim.Replace("G30-20", "G30/20").Replace("G30-75", "G30/75").Replace("G50", "G4735"));
        //        }
        //    }
        //    private double? ParseFraction(string v)
        //    {
        //        if (v == "1 34")
        //        {
        //            v = "1 3/4";
        //        }
        //        if (string.IsNullOrWhiteSpace(v)) return null;

        //        if (v.Contains('.') && !v.Contains('/'))
        //        {

        //            return double.Parse(v);
        //        }
        //        else if (!v.Contains('.') && !v.Contains('/'))
        //        {
        //            var wholenum = Int32.Parse(v);
        //            return (double)wholenum;
        //        }

        //        //Now we have two problems
        //        var r = new Regex("([0-9]+\\s+)?([1-9][0-9]*)\\s*\\/\\s*([1-9][0-9]*)");
        //        var match = r.Match(v);
        //        if (match.Success)
        //        {
        //            double whole = string.IsNullOrWhiteSpace(match.Groups[1].Value) ? 0 : double.Parse(match.Groups[1].Value);
        //            double numerator = double.Parse(match.Groups[2].Value);
        //            double denominator = double.Parse(match.Groups[3].Value);

        //            return whole + (numerator / denominator);

        //        }
        //        throw new ArgumentException($"Unable to parse fraction {v}");
        //    }

        //    private static string GetMaterialDescription(string graphite, string scrim)
        //    {
        //        string result = string.Empty;
        //        if (!string.IsNullOrWhiteSpace(graphite))
        //        {
        //            result = graphite.Replace("SAG", "").Trim();
        //        }
        //        if (!string.IsNullOrWhiteSpace(scrim))
        //        {
        //            if (result.Length > 0) result += "/";
        //            result += scrim.Replace("SAG", "").Trim();
        //        }
        //        return result;
        //    }

        //    protected override void OnModelCreating(ModelBuilder modelBuilder)
        //    {
        //        modelBuilder.Entity<SubaStart>().HasNoKey();
        //        modelBuilder.Entity<WarrantyDemand>().HasNoKey();
        //        modelBuilder.Entity<BlankView>().HasNoKey();
        //        modelBuilder.Entity<RnDBlankView>().HasNoKey();
        //        modelBuilder.Entity<BlankFerruleView>().HasNoKey();
        //        modelBuilder.Entity<RnDBlankFerruleView>().HasNoKey();
        //        modelBuilder.Entity<BomLine>().HasNoKey();
        //        modelBuilder.Entity<BomOnHandQuantity>().HasNoKey();
        //        modelBuilder.Entity<MaterialMap>().HasNoKey();


        //        modelBuilder
        //            .Entity<BLSReport>(eb =>
        //            {
        //                eb.HasKey(x => x.Sku);
        //            });
        //        modelBuilder
        //            .Entity<InputTargetBLS>(eb =>
        //            {
        //                eb.HasKey(x => x.ItemNumber);
        //            });
        //        modelBuilder
        //            .Entity<TargetBLS>(eb =>
        //            {
        //                eb.HasKey(x => new { x.Sku, x.Date });
        //                //eb.HasKey(x => );
        //            });
        //        modelBuilder
        //            .Entity<Blank>(eb =>
        //            {
        //                eb.HasKey(x => x.Id);
        //            });

        //        modelBuilder
        //            .Entity<BlankPattern>(eb =>
        //            {
        //                eb.HasKey(x => x.Id);
        //                eb.HasOne(t => t.Blank)
        //                    .WithMany(t => t.BlankPatterns)
        //                    .HasForeignKey(d => d.BlankId);
        //            });

        //        modelBuilder
        //            .Entity<BlankPatternDimension>(eb =>
        //            {
        //                eb.HasKey(x => x.Id);
        //                eb.HasOne(t => t.BlankPattern)
        //                    .WithMany(t => t.BlankPatternDimensions)
        //                    .HasForeignKey(d => d.BlankPatternId);
        //            });

        //        modelBuilder
        //           .Entity<ScrapCode>(eb =>
        //           {
        //               eb.HasKey(x => x.Reason);
        //           });
        //        modelBuilder
        //           .Entity<Mandrel>(eb =>
        //           {
        //               eb.HasKey(x => x.Name);
        //           });
        //        modelBuilder
        //           .Entity<BlankWorkOrderExclusion>(eb =>
        //           {
        //               eb.HasKey(x => x.Sku);
        //           });
        //        modelBuilder
        //           .Entity<BlankWorkOrderException>(eb =>
        //           {
        //               eb.HasKey(x => x.ScheduledSkipDate);
        //           });
        //        modelBuilder
        //          .Entity<BlankWorkOrderSetting>(eb =>
        //          {
        //              eb.HasKey(x => x.Id);
        //          });
        //        modelBuilder.Entity<ExpediteReport>().HasNoKey();
        //        base.OnModelCreating(modelBuilder);
        //    }
        //    public async Task<int> SaveChangesAsync()
        //    {
        //        foreach (var change in ChangeTracker.Entries())
        //        {
        //            var entity = change.Entity as IAuditEntity;
        //            if (change.State == EntityState.Added && entity != null)
        //            {
        //                entity.Created = DateTime.Now;
        //                entity.CreatedBy = "BlankMPS";
        //            }

        //            if (entity != null && change.State != EntityState.Deleted)
        //            {
        //                entity.Modified = DateTime.Now;
        //                entity.ModifiedBy = "BlankMPS";
        //            }

        //            if (change.Entity != null && !(change.Entity is AuditTrail))
        //            {
        //                //this.AuditTrails.Add(CreateAuditTrail(change, _user));
        //            }
        //        }
        //        try
        //        {
        //            return await base.SaveChangesAsync();
        //        }
        //        catch (Exception vex)
        //        {
        //            StringBuilder sb = new StringBuilder();

        //            //foreach (var failure in vex.EntityValidationErrors)
        //            //{
        //            //    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
        //            //    foreach (var error in failure.ValidationErrors)
        //            //    {
        //            //        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
        //            //        sb.AppendLine();
        //            //    }
        //            //}

        //            throw new Exception(
        //                "Entity Validation Failed - errors follow:\n" +
        //                sb.ToString(), vex
        //            );
        //        }
        //    }
        //    public override int SaveChanges()
        //    {
        //        foreach (var change in ChangeTracker.Entries())
        //        {
        //            var entity = change.Entity as IAuditEntity;
        //            if (change.State == EntityState.Added && entity != null)
        //            {
        //                entity.Created = DateTime.Now;
        //                entity.CreatedBy = "BlankMPS";
        //            }

        //            if (entity != null && change.State != EntityState.Deleted)
        //            {
        //                entity.Modified = DateTime.Now;
        //                entity.ModifiedBy = "BlankMPS";
        //            }

        //            if (change.Entity != null && !(change.Entity is AuditTrail))
        //            {
        //                //this.AuditTrails.Add(CreateAuditTrail(change, _user));
        //            }
        //        }
        //        try
        //        {
        //            return base.SaveChanges();
        //        }
        //        catch (Exception vex)
        //        {
        //            StringBuilder sb = new StringBuilder();

        //            //foreach (var failure in vex.InnerException)
        //            //{
        //            //    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
        //            //    foreach (var error in failure.ValidationErrors)
        //            //    {
        //            //        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
        //            //        sb.AppendLine();
        //            //    }
        //            //}

        //            throw new Exception(
        //                "Entity Validation Failed - errors follow:\n" +
        //                sb.ToString(), vex
        //            );
        //        }
        //    }

        //    public void UpdateMandrelConsumption()
        //    {
        //        foreach (var mandrel in Mandrels.Where(m => m.Quantity > 0))
        //        {
        //            mandrel.PriorConsumedPM = mandrel.ConsumedPM;
        //            mandrel.ConsumedAM = 0;
        //            mandrel.ConsumedPM = 0;
        //        }
        //        SaveChanges();
        //    }
        //    public void ClearMandrelConsumption()
        //    {
        //        foreach (var mandrel in Mandrels.Where(m => m.Quantity > 0))
        //        {
        //            mandrel.PriorConsumedPM = 0;
        //            mandrel.ConsumedAM = 0;
        //            mandrel.ConsumedPM = 0;
        //        }
        //        SaveChanges();
        //    }

        //    public void UpdateExceptions(string now)
        //    {
        //        var excpetion = Exceptions.SingleOrDefault(e => e.ScheduledSkipDate == now);
        //        Exceptions.Remove(excpetion);
        //        SaveChanges();
        //    }

        //    private class WorkOrderRecord
        //    {
        //        public string Material { get; set; }
        //        public string Graphite { get; set; }
        //        public string Scrim { get; set; }
        //        public string WorkOrder { get; set; }
        //        public string Sku { get; set; }
        //        public string Description { get; set; }
        //        public int QuantityInQueue { get; set; }
        //        public int PatternNumber { get; set; }
        //        public int DimensionNumber { get; set; }
        //        public decimal Offset { get; set; }
        //        public decimal Height { get; set; }
        //    }
    }
}
