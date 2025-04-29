using LibreriaClassi;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;
using System;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            rpPianiStudio.ItemDataBound += new RepeaterItemEventHandler(rpPianiStudio_ItemDataBound);
        }
        CaricaPianiStudio();
       
    }
    protected void CaricaPianiStudio()
    {
        
        PIANISTUDIO ps = new PIANISTUDIO();
        rpPianiStudio.DataSource = ps.MostraLista(); 
        rpPianiStudio.DataBind();
    }
    protected void rpPianiStudio_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {

            var piano = (DataRowView)e.Item.DataItem;
            string idPiano = piano["K_PianoStudio"].ToString(); // cambia col tuo nome colonna

            Repeater rpEsami = (Repeater)e.Item.FindControl("rpEsami");

            PIANISTUDIO ps = new PIANISTUDIO();
            ps.K_PianoStudio = Guid.Parse(idPiano);
            rpEsami.DataSource = ps.ElencoEsamiPianoStudio(); 
            rpEsami.DataBind();
        }
    }
    

}

