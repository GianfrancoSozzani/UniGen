using System;
using System.Data;
using System.Runtime.Remoting.Messaging;
using System.Web.UI.WebControls;
using LibreriaClassi;

public partial class _Default : System.Web.UI.Page
{
    string annoscelto = "0";
    string studenti;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PopolaAnni();
            CaricaChar();
        }
    }

    protected void PopolaAnni()
    {
        STUDENTI s = new STUDENTI();
        ddlAnno.DataSource = s.SelezionaAnno();
        ddlAnno.DataTextField = "AnnoImmatricolazione";
        ddlAnno.DataValueField = "AnnoImmatricolazione";
        ddlAnno.DataBind();
        ddlAnno.Items.Insert(0, new ListItem("Ogni anno", "0"));
    }

    protected void CaricaChar()
    {
        STUDENTI s = new STUDENTI();

        Chart1.ChartAreas["ChartArea1"].AxisX.Title = "Anno";
        Chart1.ChartAreas["ChartArea1"].AxisY.Title = "Numero di Immatricolati";
        Chart1.Series["Series1"].IsValueShownAsLabel = true;

        Chart1.Series["Series1"].Color = System.Drawing.Color.SteelBlue;
        Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;

        DataTable dt = new DataTable();
        if (annoscelto == "0")
        {
            dt = s.SelezionaAnno();
            Chart1.Series["Series1"].XValueMember = "AnnoImmatricolazione";
        }
        else
        {

            dt = s.SelezionaAnnoSingolo(annoscelto);
            Chart1.Series["Series1"].XValueMember = "AnnoImmatricolazione";

        }

        if (dt != null && dt.Rows.Count > 0)
        {
            Chart1.DataSource = dt;
            Chart1.Series["Series1"].YValueMembers = "NumeroStudenti";
            Chart1.DataBind();

            rptIscritti.DataSource = dt; 
            rptIscritti.DataBind();

        }
        else
        {
            Chart1.DataSource = null;
            Chart1.DataBind();
            rptIscritti.DataSource = null;
            rptIscritti.DataBind();
        }

    }

    protected void ddlAnno_SelectedIndexChanged(object sender, EventArgs e)
    {
        annoscelto = ddlAnno.SelectedValue;
        CaricaChar();
    }

    protected void btnRiepilogo_Click(object sender, EventArgs e)
    {
        annoscelto = "0";
        PopolaAnni();
        CaricaChar();
    }

    protected void CaricaRp()
    {
        STUDENTI s = new STUDENTI();
        studenti = "NumeroStudenti";
        s.StudentiIscritti();
        if (annoscelto != "0")
        {
            rptIscritti.DataSource = studenti;
            rptIscritti.DataBind();

        }
    }
}