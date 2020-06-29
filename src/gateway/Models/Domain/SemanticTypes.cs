using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace GDS.Gateway.Models.Domain
{

    public enum SemanticGroups
    {

        [EnumMember(Value = "NUMERIC")] //  Numeric group
        NUMERIC = 0,
        [EnumMember(Value = "DATETIME")] //     DateTime group
        DATETIME = 1,
        [EnumMember(Value = "GEO")] //  Geo Group
        GEO = 2,
        [EnumMember(Value = "CURRENCY")] //     Currency Group
        CURRENCY = 3
    }

    public enum SemanticTypes
    {
        [EnumMember(Value = "TEXT")] //     Free form text	"Here is some text"
        TEXT = 0,

        [EnumMember(Value = "NUMBER")] //   Decimal Number	14
        NUMBER = 1,

        [EnumMember(Value = "YEAR")] //    YYYY	"2017"
        YEAR,
[EnumMember(Value = "YEAR_QUARTER")] //     YYYYQ	"20171"
        YEAR_QUARTER,
        [EnumMember(Value = "YEAR_MONTH")] //   YYYYMM	"201703"
        YEAR_MONTH,
        [EnumMember(Value = "YEAR_WEEK")] //    YYYYww	"201707"
        YEAR_WEEK,
        [EnumMember(Value = "YEAR_MONTH_DAY")] //   YYYYMMDD	"20170317"
        YEAR_MONTH_DAY,
        [EnumMember(Value = "YEAR_MONTH_DAY_HOUR")] //  YYYYMMDDHH	"2017031403"
        YEAR_MONTH_DAY_HOUR,
        [EnumMember(Value = "YEAR_MONTH_DAY_SECOND")] //    YYYYMMDDHHMMSS	"20170314031545"
        YEAR_MONTH_DAY_SECOND,
        [EnumMember(Value = "QUARTER")] // 	(1, 2, 3, 4)	"1"
        QUARTER,
        [EnumMember(Value = "MONTH")] //    MM	"03"
        MONTH,
        [EnumMember(Value = "WEEK")] //     ww	"07"
        WEEK,
        [EnumMember(Value = "MONTH_DAY")] //    MMDD	"0317"
        MONTH_DAY,
        [EnumMember(Value = "DAY_OF_WEEK")] //  A decimal number 0-6 with 0 representing Sunday	"0"
        DAY_OF_WEEK,
        [EnumMember(Value = "DAY")] // 	DD "17"
        DAY,
        [EnumMember(Value = "HOUR")] //     HH	"02"
        HOUR,
        [EnumMember(Value = "MINUTE")] //   mm	"12"
        MINUTE,
        [EnumMember(Value = "DURATION")] //     A Duration of Time (in seconds)	6340918234
        DURATION,
        [EnumMember(Value = "COUNTRY")] //  Country	"United States"
        COUNTRY,
        [EnumMember(Value = "COUNTRY_CODE")] //     Country Code	"US"
        COUNTRY_CODE,
        [EnumMember(Value = "CONTINENT")] //    Continent	"Americas"
        CONTINENT,
        [EnumMember(Value = "CONTINENT_CODE")] //   Continent Code	"019"
        CONTINENT_CODE,
        [EnumMember(Value = "SUB_CONTINENT")] //    Sub Continent	"North America"
        SUB_CONTINENT,
        [EnumMember(Value = "SUB_CONTINENT_CODE")] //   Sub Continent Code	"003"
        SUB_CONTINENT_CODE,
        [EnumMember(Value = "REGION")] //   Region	"California"
        REGION,
        [EnumMember(Value = "REGION_CODE")] //  Region Code	"CA"
        REGION_CODE,
        [EnumMember(Value = "CITY")] //     City	"Mountain View"
        CITY,
        [EnumMember(Value = "CITY_CODE")] //    City Code	"1014044"
        CITY_CODE,
        [EnumMember(Value = "METRO_CODE")] //   Metro Code	"200807"
        METRO_CODE,
        [EnumMember(Value = "LATITUDE_LONGITUDE")] //   Latitude and Longitude	"51.5074, -0.1278"
        LATITUDE_LONGITUDE,
        [EnumMember(Value = "PERCENT")] //  Decimal percentage (can be over 1.0)	1.0
        PERCENT,
        [EnumMember(Value = "BOOLEAN")] // 	true or false	true
        BOOLEAN,
        [EnumMember(Value = "URL")] //  A URL as text	"https://www.google.com"
        URL,
    }
}
