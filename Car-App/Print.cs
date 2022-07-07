using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using Car_App.Data;
using Car_App.Models;
using System.Drawing.Printing;
using System.Drawing;
using System.Data;
using System.Diagnostics;

namespace Car_App
{
    public class Print
    {
        private int count = 1;
        private Font printFont;
        private readonly DataContext dataContext;

        public Print(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        private void Pd_PrintPage(object sender, PrintPageEventArgs ev)
        {
            float leftMargin = ev.MarginBounds.Left;
            float topMargin = ev.MarginBounds.Top;
            float rightMargin = ev.MarginBounds.Right;
            float firstLine = ev.Graphics.MeasureString("Registration Number", printFont).Width + leftMargin + 50;
            float secondLine = ev.Graphics.MeasureString("VIN", printFont).Width + firstLine + 220;
            float thirdLine = ev.Graphics.MeasureString("Model", printFont).Width + secondLine + 220;
            List<Car> cars = dataContext.Cars.ToList();
            float linesPerPage = ev.MarginBounds.Height /
               printFont.GetHeight(ev.Graphics);
            string titleLine = "List of Cars";
            float titlePosX = ev.MarginBounds.Left + (ev.MarginBounds.Width / 2) - (5 * titleLine.Length);
            ev.Graphics.DrawString(titleLine, printFont, Brushes.Black, titlePosX, topMargin, new StringFormat());
            ev.Graphics.DrawString("Registration Number", printFont, Brushes.Black, new PointF(leftMargin, topMargin + 16));
            ev.Graphics.DrawString("VIN", printFont, Brushes.Black, new PointF(firstLine, topMargin + 16));
            ev.Graphics.DrawString("Model", printFont, Brushes.Black, new PointF(secondLine, topMargin + 16));
            ev.Graphics.DrawString("Brand", printFont, Brushes.Black, new PointF(thirdLine, topMargin + 16));
            foreach (Car car in cars)
            {
                if (count > linesPerPage)
                {
                    ev.HasMorePages = true;
                    return;
                }
                float yPos = topMargin + (count * printFont.GetHeight(ev.Graphics));
                ev.Graphics.DrawString(car.RegistrationNumber, printFont, Brushes.Black, new PointF(leftMargin, yPos + 15.5F));
                ev.Graphics.DrawString(car.VIN, printFont, Brushes.Black, new PointF(firstLine, yPos + 15.5F));
                ev.Graphics.DrawString(car.Model, printFont, Brushes.Black, new PointF(secondLine, yPos + 15.5F));
                ev.Graphics.DrawString(car.Brand, printFont, Brushes.Black, new PointF(thirdLine, yPos + 15.5F));
                count++;
            }
            ev.HasMorePages = false;
            float TexBlockHeight = (int)ev.Graphics.MeasureString("Registration Number", printFont).Height * (count + 1);
            Pen blackPen = new(Color.Black, 1);
            float size = topMargin;
            for (int i = 15; i < TexBlockHeight; i += 16)
            {
                ev.Graphics.DrawLine(blackPen, new PointF(leftMargin, topMargin + i), new PointF(rightMargin, topMargin + i));
                size += 16;
            }
            ev.Graphics.DrawLine(blackPen, new PointF(firstLine, topMargin + 15), new PointF(firstLine, size));
            ev.Graphics.DrawLine(blackPen, new PointF(secondLine, topMargin + 15), new PointF(secondLine, size));
            ev.Graphics.DrawLine(blackPen, new PointF(thirdLine, topMargin + 15), new PointF(thirdLine, size));
            ev.Graphics.DrawLine(blackPen, new PointF(leftMargin, topMargin + 15), new PointF(leftMargin, size));
            ev.Graphics.DrawLine(blackPen, new PointF(rightMargin, topMargin + 15), new PointF(rightMargin, size));
        }
        public void StartPrint()
        {
            try
            {
                printFont = new Font("Arial", 10);
                PrintDocument pd = new();
                pd.PrintPage += new PrintPageEventHandler(Pd_PrintPage);
                pd.DefaultPageSettings.Landscape = true;
                pd.Print();
                pd.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}