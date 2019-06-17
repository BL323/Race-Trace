using System;
using System.Windows.Media;
using Client.Trace;

namespace Client.Utilities
{
    /// <summary>
    /// Provides a mapping for team names to their specific colour.
    /// </summary>
    public static class TeamColourProvider
    {
        private static readonly Random Random = new Random();

        internal static Color ColourForTeam(string team)
        {
            switch (team)
            {
                case Teams.AlfaRomeo:
                case Teams.Sauber:
                    return Colors.White;
                case Teams.Ferrari:
                    return Colors.Red;
                case Teams.RacingPoint:
                case Teams.ForceIndia:
                    return Colors.HotPink;
                case Teams.Haas:
                    return Colors.DarkOrchid;
                case Teams.McLaren:
                    return Colors.OrangeRed;
                case Teams.Mercedes:
                    return Colors.ForestGreen;
                case Teams.RedBull:
                    return Colors.DodgerBlue;
                case Teams.Renault:
                    return Colors.Yellow;
                case Teams.ToroRosso:
                    return Colors.Gray;
                case Teams.Williams:
                    return Colors.SaddleBrown;

                default:
                    return GenerateRandomColour();
            }
        }

        private static Color GenerateRandomColour()
        {
            var rgbBytes = new byte[3];
            Random.NextBytes(rgbBytes);
            return Color.FromRgb(rgbBytes[0], rgbBytes[1], rgbBytes[2]);
        }
    }
}
