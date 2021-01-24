/*
 * PatreonList - https://github.com/Rosalie241/PatreonList
 *  Copyright (C) 2021 Rosalie Wanders <rosalie@mailbox.org>
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License version 3.
 *  You should have received a copy of the GNU General Public License
 *  along with this program. If not, see <https://www.gnu.org/licenses/>.
 */
using CsvHelper.Configuration.Attributes;

namespace PatreonList
{
    public class Patron
    {
        [Name("Name")]
        public string Name { get; set; }
        [Name("Pledge Amount")]
        public double Amount { get; set; }
        [Name("Lifetime Amount")]
        public double LifetimeAmount { get; set; }
        [Name("Patron Status")]
        public string Status { get; set; }
    }
}
