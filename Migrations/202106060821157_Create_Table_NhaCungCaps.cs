namespace UongHoangAnh_06.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_Table_NhaCungCaps : DbMigration
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.NhaCungCaps");
        }
    }
}
