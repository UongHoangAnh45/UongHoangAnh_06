using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UongHoangAnh_06.Models;

namespace UongHoangAnh_06.Controllers
{
    public class SanPhamsController : Controller
    {
        private LTQLDb db = new LTQLDb();

        // GET: SanPhams
        public ActionResult Index()
        {
            return View(db.NhaCungCaps.ToList());
        }

        // GET: SanPhams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = (SanPham)db.NhaCungCaps.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // GET: SanPhams/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNhaCungCap,TenNhaCungCap,MaSanPham,TenSanPham")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.NhaCungCaps.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sanPham);
        }

        // GET: SanPhams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = (SanPham)db.NhaCungCaps.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: SanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNhaCungCap,TenNhaCungCap,MaSanPham,TenSanPham")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sanPham);
        }

        // GET: SanPhams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = (SanPham)db.NhaCungCaps.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }


        // POST: SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SanPham sanPham = (SanPham)db.NhaCungCaps.Find(id);
            db.NhaCungCaps.Remove(sanPham);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //upload file
        public ActionResult UploadFlie(HttpPostedFileBase file)
        {
            //dat ten cho file
            string _FileName = "SanPham.xlsx";
            //duong dan luu file
            string _path = Path.Combine(Server.MapPath("~/Uploads/Excels"), _FileName);
            //luu file len server
            file.SaveAs(_path);
            //doc du lieu tu file excel
            DataTable dt = ReadDataFromExcelFile(_path);
            //CopyDataByBulk(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SanPham kh = new SanPham();
                kh. = dt.Rows[i][0].ToString();
                kh.= dt.Rows[i][1].ToString();
                kh. = dt.Rows[i][2].ToString();
                db.SanPhams.Add(kh);
                db.SaveChanges();

            }
            return View("Index");

        }
        //doc file excel tra ve du lieu dang datatable
        public DataTable ReadDataFromExcelFile(string filepath)
        {
            string connectionString = "";
            string fileExtention = filepath.Substring(filepath.Length - 4).ToLower();
            if (fileExtention.IndexOf("xlsx") == 0)
            {
                connectionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source =" + filepath + ";Extended Properties=\"Excel 12.0 Xml;HDR=NO\"";
            }
            else if (fileExtention.IndexOf("xls") == 0)
            {
                connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filepath + ";Extended Properties=Excel 8.0";
            }

            // Tạo đối tượng kết nối
            OleDbConnection oledbConn = new OleDbConnection(connectionString);
            DataTable data = null;
            try
            {
                // Mở kết nối
                oledbConn.Open();

                // Tạo đối tượng OleDBCommand và query data từ sheet có tên "Sheet1"
                OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Sheet1$]", oledbConn);

                // Tạo đối tượng OleDbDataAdapter để thực thi việc query lấy dữ liệu từ tập tin excel
                OleDbDataAdapter oleda = new OleDbDataAdapter();

                oleda.SelectCommand = cmd;

                // Tạo đối tượng DataSet để hứng dữ liệu từ tập tin excel
                DataSet ds = new DataSet();

                // Đổ đữ liệu từ tập excel vào DataSet
                oleda.Fill(ds);

                data = ds.Tables[0];
            }
            catch
            {
            }
            finally
            {
                // Đóng chuỗi kết nối
                oledbConn.Close();
            }
            return data;
        }

        private void CopyDataByBulk(DataTable dt)
        {
            //lay ket noi voi database luu trong file webconfig
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB Context"].ConnectionString);
            SqlBulkCopy bulkcopy = new SqlBulkCopy(con);
            bulkcopy.DestinationTableName = "SanPhams";
            bulkcopy.ColumnMappings.Add(0, "");
            bulkcopy.ColumnMappings.Add(1, "");
            bulkcopy.ColumnMappings.Add(2, "");

            con.Open();
            bulkcopy.WriteToServer(dt);
            con.Close();
        }
        //upload file excel
        [HttpPost]
        public ActionResult SanPhams (HttpPostedFileBase file)
        {
            try
            {
                //upload file thanh cong va file co du lieu
                if (file.ContentLength > 0)
                {
                    //Your code
                }
            }
            catch (Exception )
            {
                //neu upload file that bai
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
