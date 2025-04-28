using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibreriaClassi;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Supponiamo che tu abbia già la tua DataTable chiamata 'miaDataTable'
        // con colonne: Corso (string) e Importo (decimal)
        PAGAMENTI paga = new PAGAMENTI();
        DataTable miaDataTable = paga.IncassoAnnoCorrente();
        Chart1.Series["Series1"].Points.Clear(); // Pulisce i punti, nel caso di refresh

        foreach (DataRow row in miaDataTable.Rows)
        {
            string corso = row["Anno"].ToString();
            decimal importo = Convert.ToDecimal(row["Importo"]);

            Chart1.Series["Series1"].Points.AddXY(corso, importo);
        }

        // Personalizzazione grafico
        Chart1.ChartAreas["ChartArea1"].AxisX.Title = "Anno";
        Chart1.ChartAreas["ChartArea1"].AxisY.Title = "Importo Pagato (€)";
        Chart1.Series["Series1"].IsValueShownAsLabel = true; // mostra valore sopra la colonna

        // Migliorie estetiche opzionali:
        Chart1.Series["Series1"].Color = System.Drawing.Color.SteelBlue;
        Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1; // forza a mostrare ogni etichetta
    }

}