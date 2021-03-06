// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApp.Data;

#nullable disable

namespace WebApp.Migrations
{
    [DbContext(typeof(WebAppContext))]
    [Migration("20220128143903_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("WebApp.Models.Medarbejder", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<int>("alder")
                        .HasColumnType("int");

                    b.Property<string>("eMail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("navn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("telefonNummer")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("Medarbejder");
                });

            modelBuilder.Entity("WebApp.Models.Opgaver", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<int>("medarbejder")
                        .HasColumnType("int");

                    b.Property<int>("minAlder")
                        .HasColumnType("int");

                    b.Property<string>("navn")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Opgaver");
                });
#pragma warning restore 612, 618
        }
    }
}
