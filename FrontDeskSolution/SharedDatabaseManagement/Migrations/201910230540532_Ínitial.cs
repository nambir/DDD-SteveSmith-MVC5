namespace VetOffice.SharedDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ínitial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ScheduleId = c.Guid(nullable: false),
                        ClientId = c.Int(nullable: false),
                        PatientId = c.Int(nullable: false),
                        RoomId = c.Int(nullable: false),
                        DoctorId = c.Int(),
                        TimeRange_Start = c.DateTime(nullable: false),
                        TimeRange_End = c.DateTime(nullable: false),
                        AppointmentTypeId = c.Int(nullable: false),
                        Title = c.String(),
                        DateTimeConfirmed = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Schedules", t => t.ScheduleId, cascadeDelete: true)
                .Index(t => t.ScheduleId);
            
            CreateTable(
                "dbo.AppointmentTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Code = c.String(),
                        Duration = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                        Salutation = c.String(),
                        EmailAddress = c.String(),
                        PreferredName = c.String(),
                        PreferredDoctorId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        Name = c.String(),
                        Gender = c.Int(nullable: false),
                        AnimalType_Species = c.String(),
                        AnimalType_Breed = c.String(),
                        PreferredDoctorId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Schedules",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ClinicId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointments", "ScheduleId", "dbo.Schedules");
            DropForeignKey("dbo.Patients", "ClientId", "dbo.Clients");
            DropIndex("dbo.Appointments", new[] { "ScheduleId" });
            DropIndex("dbo.Patients", new[] { "ClientId" });
            DropTable("dbo.Schedules");
            DropTable("dbo.Rooms");
            DropTable("dbo.Doctors");
            DropTable("dbo.Patients");
            DropTable("dbo.Clients");
            DropTable("dbo.AppointmentTypes");
            DropTable("dbo.Appointments");
        }
    }
}
