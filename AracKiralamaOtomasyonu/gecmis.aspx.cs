using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AracKiralamaOtomasyonu
{
    public partial class gecmis : System.Web.UI.Page
    {
       protected void Page_Load(object sender, EventArgs e)
        {
            ortak_fonksiyonlar.DisablePageCaching();
        }
    }
}