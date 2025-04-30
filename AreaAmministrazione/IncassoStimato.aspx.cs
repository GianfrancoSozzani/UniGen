using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;
using LibreriaClassi;

public partial class _Default : System.Web.UI.Page
{
    string annoscelto = DateTime.Now.Year.ToString();
    protected Guid facolta = Guid.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CaricaChart();
            CaricaFacolta();
            CaricaRp();
        }
    }
    public void CaricaChart()
    {
        PAGAMENTI paga = new PAGAMENTI();
        paga.Anno = annoscelto;
        if (facolta == Guid.Empty)
        {
            DataTable dT = paga.IncassiStimatiFacolta();
            Chart1.Series["Series1"].Points.Clear();
            foreach (DataRow row in dT.Rows)
            {
                string corso = row["Facolta"].ToString();
                decimal stima = Convert.ToDecimal(row["Stima"]);

                Chart1.Series["Series1"].Points.AddXY(corso, stima);
            }
            Chart1.ChartAreas["ChartArea1"].AxisX.Title = "Anno";
            Chart1.ChartAreas["ChartArea1"].AxisY.Title = "Importo Pagato (€)";
            Chart1.Series["Series1"].IsValueShownAsLabel = true;

            Chart1.Series["Series1"].Color = System.Drawing.Color.SteelBlue;
            Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
        }
        else if (facolta != Guid.Empty)
        {
            DataTable dT = paga.IncassiStimatiCorso(facolta);
            Chart1.Series["Series1"].Points.Clear();

            foreach (DataRow row in dT.Rows)
            {
                string corso = row["Corso"].ToString();
                decimal stima = Convert.ToDecimal(row["Stima"]);

                Chart1.Series["Series1"].Points.AddXY(corso, stima);
            }
            Chart1.ChartAreas["ChartArea1"].AxisX.Title = "Corso";
            Chart1.ChartAreas["ChartArea1"].AxisY.Title = "Importo Pagato (€)";
            Chart1.Series["Series1"].IsValueShownAsLabel = true;

            Chart1.Series["Series1"].Color = System.Drawing.Color.SteelBlue;
            Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
        }
        DataBind();
    }
    protected void btnRiepilogo_Click(object sender, EventArgs e)
    {
        facolta = Guid.Empty;
        CaricaChart();
        CaricaRp();
    }
    public void CaricaRp()
    {
        PAGAMENTI pag = new PAGAMENTI();
        pag.Anno = annoscelto;
        DataTable dt = new DataTable();
        if (facolta == Guid.Empty)
        {

                dt = pag.IncassiStimatiFacolta();
        }
        else
        {
            dt = pag.IncassiStimatiCorso(facolta);
        }
        decimal totaleImporto = 0;
        decimal totaleStima = 0;
        int totaleIscritti = 0;

        foreach (DataRow row in dt.Rows)
        {
            totaleImporto += row.Field<decimal>("Incasso");
            totaleStima += row.Field<decimal>("Stima");
            totaleIscritti += row.Field<int>("Iscritti");
        }

        // Aggiungi riga totale
        DataRow totaleRow = dt.NewRow();
        totaleRow["Incasso"] = totaleImporto;
        totaleRow["Stima"] = totaleStima;
        totaleRow["Iscritti"] = totaleIscritti;
        totaleRow[facolta == Guid.Empty ? "Facolta" : "Corso"] = "TOTALE";

        dt.Rows.InsertAt(totaleRow, 0); // Inserisce in cima

        rptIncassiFacolta.DataSource = dt;
        rptIncassiFacolta.DataBind();
    }

    public void CaricaFacolta()
    {
        FACOLTA fa = new FACOLTA();
        ddlFacolta.DataSource = fa.SelezionaTutto();
        ddlFacolta.DataTextField = "TitoloFacolta";
        ddlFacolta.DataValueField = "K_Facolta";
        ddlFacolta.DataBind();
        ddlFacolta.Items.Insert(0, new ListItem("Seleziona Facoltà", ""));
    }

    protected void ddlFacolta_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFacolta.SelectedValue == "")
        {
            facolta = Guid.Empty;
        }
        else
        {
            facolta = Guid.Parse(ddlFacolta.SelectedValue);
        }
        CaricaChart();
        CaricaRp();
    }
}