namespace UongHoangAnh_06.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_Table_SanPhams : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NhaCungCaps",
                c => new
                    {
                        MaNhaCungCap = c.Int(nullable: false, identity: true),
                        TenNhaCungCap = c.String(maxLength: 128, fixedLength: true, unicode: false),
                    })
                .PrimaryKey(t => t.MaNhaCungCap);
            
            CreateTable(
                "dbo.SanPhams",
                c => new
                    {
                        MaNhaCungCap = c.Int(nullable: false),
                        MaSanPham = c.Int(nullable: false),
                        TenSanPham = c.String(),
                    })
                .PrimaryKey(t => t.MaNhaCungCap)
                .ForeignKey("dbo.NhaCungCaps", t => t.MaNhaCungCap)
                .Index(t => t.MaNhaCungCap);
            
            DropTable("dbo.NhaCungCaps");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.NhaCungCaps",
                c => new
                    {
                        MaNhaCungCap = c.Int(nullable: false, identity: true),
                        TenNhaCungCap = c.String(maxLength: 128, fixedLength: true, unicode: false),
                    })
                .PrimaryKey(t => t.MaNhaCungCap);
            
            DropForeignKey("dbo.SanPhams", "MaNhaCungCap", "dbo.NhaCungCaps");
            DropIndex("dbo.SanPhams", new[] { "MaNhaCungCap" });
            DropTable("dbo.SanPhams");
            DropTable("dbo.NhaCungCaps");
        }
    }
}
