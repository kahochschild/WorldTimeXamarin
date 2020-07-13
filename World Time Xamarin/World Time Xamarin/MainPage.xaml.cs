using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace World_Time_Xamarin
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
			var sv = new WorldTimeWebService();
			var es = await sv.GetTimeZonesAsync();
			int timezoneIndex = 0;
            sl1.IsVisible = true;
			Xamarin.Forms.Device.BeginInvokeOnMainThread(() => {
                pkrTimeZones.Items.Add("");
				for (int x = 0; x < es.Length; x++)
				{
					pkrTimeZones.Items.Add(es[x].Name);
				}
				pkrTimeZones.SelectedIndex = timezoneIndex;

			});
		}

        private async void pkrTimeZones_SelectedIndexChanged(object sender, EventArgs e)
        {
            string value = pkrTimeZones.Items[pkrTimeZones.SelectedIndex].ToString();

            if (string.IsNullOrWhiteSpace(value))
            {
                sl2.IsVisible = false;
            }
            else
            {
                var sv = new WorldTimeWebService();
                var es = await sv.GetTimeZoneResultAsync(value);

                sl2.IsVisible = true;

                var dstfrom = "DST From: " + es.dst_from;
                var dstuntil = "DST Until: " + es.dst_from;

                lblAbbrev.Text = "Abbrev: " + es.abbreviation;
                lblClientIP.Text = "Client IP: " + es.client_ip;
                lblDateTime.Text = "Date Time: " + es.datetime;
                lblDayofWeek.Text = "Day of Week: " + es.day_of_week.ToString();
                lblDayofYear.Text = "Day of Year: " + es.day_of_year.ToString();
                lblDST.Text = "DST: " + es.dst.ToString();
                lblDSTFrom.Text = es.dst_from == null ? "" : dstfrom;
                lblDSTOffset.Text = "DST Offset: " + es.dst_offset.ToString();
                lblDSTUntil.Text = es.dst_until == null ? "" : dstuntil;
                lblRawOffset.Text = "Raw Offset: " + es.raw_offset.ToString();
                lblTimeZone.Text = "Time Zone: " + es.timezone;
                lblUnixTime.Text = "Unix Time: " + es.unixtime.ToString();
                lblUTCDateTime.Text = "UTC Date Time: " + es.utc_datetime;
                lblUTCOffset.Text = "UTC Offset: " + es.utc_offset.ToString();
                lblWeekNumber.Text = "Week Number: " + es.week_number.ToString();

            }

        }
    }
}
