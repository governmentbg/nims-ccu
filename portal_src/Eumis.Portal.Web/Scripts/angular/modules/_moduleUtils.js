angular.module('utils', [])
    .factory("rfc4122", function () {
        return {
            newuuid: function () {
                // http://www.ietf.org/rfc/rfc4122.txt
                var s = [];
                var hexDigits = "0123456789abcdef";
                for (var i = 0; i < 36; i++) {
                    s[i] = hexDigits.substr(Math.floor(Math.random() * 0x10), 1);
                }
                s[14] = "4";  // bits 12-15 of the time_hi_and_version field to 0010
                s[19] = hexDigits.substr((s[19] & 0x3) | 0x8, 1);  // bits 6-7 of the clock_seq_hi_and_reserved to 01
                s[8] = s[13] = s[18] = s[23] = "-";
                return s.join("");
            }
        }
    }).factory("romanize", function () {
        return {
            convert: function (num) {
                if (!+num)
                    return false;
                var digits = String(+num).split(""),
                    key = ["", "C", "CC", "CCC", "CD", "D", "DC", "DCC", "DCCC", "CM",
                           "", "X", "XX", "XXX", "XL", "L", "LX", "LXX", "LXXX", "XC",
                           "", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX"],
                    roman = "",
                    i = 3;
                while (i--)
                    roman = (key[+digits.pop() + (i * 10)] || "") + roman;
                return Array(+digits.join("") + 1).join("M") + roman;
            }
        }
    }).factory("copyAddress", function () {
        return {
            copySeatAddress: function (company) {
                var EmptyItem = { id: '', text: '', Code: '', Name: '' };

                company.Correspondence = {};

                // pass validation flags
                company.Correspondence.IsCountryValid = company.Seat.IsCountryValid;
                company.Correspondence.IsSettlementValid = company.Seat.IsSettlementValid;
                company.Correspondence.IsPostCodeValid = company.Seat.IsPostCodeValid;
                company.Correspondence.IsStreetValid = company.Seat.IsStreetValid;
                company.Correspondence.IsFullAddressValid = company.Seat.IsFullAddressValid;

                company.Correspondence.Country = EmptyItem;
                company.Correspondence.Settlement = EmptyItem;

                if (company.Seat.Country) {
                    company.Correspondence.Country = {
                        id: company.Seat.Country.id,
                        text: company.Seat.Country.text,
                        Code: company.Seat.Country.Code,
                        Name: company.Seat.Country.Name
                    };
                }

                if (company.Seat.Settlement) {
                    company.Correspondence.Settlement = {
                        id: company.Seat.Settlement.id,
                        text: company.Seat.Settlement.text,
                        Code: company.Seat.Settlement.Code,
                        Name: company.Seat.Settlement.Name
                    };
                }

                if (company.Seat) {
                    company.Correspondence.PostCode = company.Seat.PostCode;
                    company.Correspondence.Street = company.Seat.Street;
                    company.Correspondence.FullAddress = company.Seat.FullAddress;
                }
            }
        }
    })
    .factory("dateDiff", function () {
        return {
            inMonths: function (d1, d2) {
                var d1Y = d1.getFullYear();
                var d2Y = d2.getFullYear();
                var d1M = d1.getMonth();
                var d2M = d2.getMonth();
                var d1D = d1.getDate();
                var d2D = d2.getDate();

                var result = (d2M + 12 * d2Y) - (d1M + 12 * d1Y);

                if (d2D > d1D) {
                    result++;
                }

                return result;
            }
        }
    });