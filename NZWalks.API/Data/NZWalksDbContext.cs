using Microsoft.EntityFrameworkCore;
using NZWalks.API.Model.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions dbContextOptions):base(dbContextOptions) 
        {

        }


        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id= Guid.Parse("a1e439f5-1367-4c1f-a9dd-f5bc481993c8") ,
                    Name= "Easy"
                },
                 new Difficulty()
                {
                    Id= Guid.Parse("12001abb-fe0b-46de-ae53-d2f82845a91b"),
                    Name= "Medium"
                },
                  new Difficulty()
                {
                    Id= Guid.Parse("7ceb9dea-ffc8-4ecd-8379-3614f598844c"),
                    Name= "Hard"
                }
            };
            //Seed difficulties to the DB
            modelBuilder.Entity<Difficulty>().HasData(difficulties);


            var regions = new List<Region>()
            {
                new Region()
                {
                    Id = Guid.Parse("7d4b3021-4ed4-4b47-9cb5-e9353d991539"),
                    Name = "Auckland",
                    Code = "AKL",
                    RegionImageUrl= "https://upload.wikimedia.org/wikipedia/commons/d/d1/Auckland_Region_map_EN.png"
                },
                new Region()
                {
                    Id = Guid.Parse("d4ffb7b8-579f-4dca-a9a1-4d5d0b55197c"),
                    Name ="Waikato",
                    Code = "WKO",
                    RegionImageUrl="https://upload.wikimedia.org/wikipedia/commons/thumb/5/5b/Waikato_Region_location_in_New_Zealand.svg/250px-Waikato_Region_location_in_New_Zealand.svg.png"

                },
                new Region()
                {
                     Id = Guid.Parse("05761056-cdbf-46e4-9dc4-eae8b2d4e749"),
                     Name ="Bay of Plenty",
                     Code ="BOP",
                     RegionImageUrl ="https://images.mapsofworld.com/newzealand/bay-of-plenty-map.jpg"
                },
                new Region()
                {
                     Id = Guid.Parse("790e2b23-b023-40aa-a99d-72b2e6a6cdf9"),
                     Name ="Gisborne",
                     Code ="GIS",
                     RegionImageUrl ="https://www.exploretheeastcape.co.nz/uploads/6/1/5/1/61514749/7-gisborne_orig.jpg"
                }
            };

            //Seed Regions in DB
            modelBuilder.Entity<Region>().HasData(regions);
        }

    }
}
