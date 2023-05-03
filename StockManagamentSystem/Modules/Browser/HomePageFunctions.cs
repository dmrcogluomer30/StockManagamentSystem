using StockManagamentSystem.Modules.MySQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagamentSystem.Modules.Browser
{
    internal class HomePageFunctions
    {
        public static string GetHomePage()
        {
            return GetHomePageStats() + GetHomePageTimeline();
        }

        private static string GetHomePageStats()
        {
            return GlobalProc.ReplaceHTML(
                @"<div class='row'>
                  <div class='col-12 col-sm-6 col-md-4'>
                    <div class='info-box'>
                      <span class='info-box-icon bg-info elevation-1'>
                        <i class='fas fa-info'></i>
                      </span>
                      <div class='info-box-content'>
                        <span class='info-box-text'>Yetkili Seviyeniz</span>
                        <span class='info-box-number'> " + GlobalProc.GetAdminLevelName(Data.Admin)+ @"
                        </span>
                      </div>
                    </div>
                  </div>
                  <div class='clearfix hidden-md-up'></div>
                  <div class='col-12 col-sm-6 col-md-4'>
                    <div class='info-box mb-3'>
                      <span class='info-box-icon bg-success elevation-1'>
                        <i class='fas fa-shopping-cart'></i>
                      </span>
                      <div class='info-box-content'>
                        <span class='info-box-text'>Stoktaki Toplam Ürün</span>
                        <span class='info-box-number'>" + DBContext.Instance.Stocks.Count().ToString() +@" adet</span>
                      </div>
                    </div>
                  </div>
                  <div class='col-12 col-sm-6 col-md-4'>
                    <div class='info-box mb-3'>
                      <span class='info-box-icon bg-warning elevation-1'>
                        <i class='fas fa-users'></i>
                      </span>
                      <div class='info-box-content'>
                        <span class='info-box-text'>Kullanıcılar</span>
                        <span class='info-box-number'>" + DBContext.Instance.Users.Count().ToString() +@" kişi</span>
                      </div>
                    </div>
                  </div>
                </div>");
        }

        private static string GetHomePageTimeline()
        {
            string timelineItem = "";

            List<Stocks> stocks = DBContext.Instance.Stocks.OrderByDescending(x => x.CreatedDate).Take(5).ToList();

            foreach (Stocks item in stocks)
            {
                timelineItem += @"<div>
                        <i class='fas fa-page bg-blue'></i>
                        <div class='timeline-item'>
                          <span class='time'>
                            <i class='fas fa-clock'></i>" + item.CreatedDate + @"</span>
                          <h3 class='timeline-header'>
                            Bir ürün eklendi.
                          </h3>
                          <div class='timeline-body'> " + item.CreatedDate +@" tarihinde "+ item.Name + @" isimli bir ürün eklendi.</div>
                          <div class='timeline-footer'>
                          </div>
                        </div>
                      </div>";
            }

            return GlobalProc.ReplaceHTML(@"<div class='row'>
                  <div class='col-md-12'>
                    <div class='timeline'>
                      <div class='time-label'>
                        <span class='bg-red'>Stok İşlem Detayları (Son 5 Kayıt)</span>
                      </div>
                      "+ timelineItem +@"
                      <div>
                        <i class='fas fa-clock bg-gray'></i>
                      </div>
                    </div>
                  </div>
                </div>");
        }


    }
}
