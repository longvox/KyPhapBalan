using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KyPhapBaLan
{
    class SoNguyenLon
    {

        public string Number { get; set; }

        public SoNguyenLon() { }

        public SoNguyenLon(string num)
        {
            this.Number = num;
        }

        public static implicit operator SoNguyenLon(string num)
        {
            return new SoNguyenLon(num);
        }

        public static implicit operator SoNguyenLon(long num)
        {
            return new SoNguyenLon(num.ToString());
        }

        public override string ToString()
        {
            return this.Number.ToString();
        }

        public int ToInt()
        {
            return Convert.ToInt32(this.Number);
        }

        #region Operator +-*/%^
        public static SoNguyenLon operator +(SoNguyenLon x, SoNguyenLon y)
        {
            SoNguyenLon result = new SoNguyenLon();
            result.Number = Cong(x.Number, y.Number);
            return result;
        }

        public static SoNguyenLon operator -(SoNguyenLon x, SoNguyenLon y)
        {
            SoNguyenLon result = new SoNguyenLon();
            result.Number = Tru(x.Number, y.Number);
            return result;
        }

        public static SoNguyenLon operator *(SoNguyenLon x, SoNguyenLon y)
        {
            SoNguyenLon result = new SoNguyenLon();
            result.Number = Nhan(x.Number, y.Number);
            return result;
        }

        public static SoNguyenLon operator /(SoNguyenLon x, SoNguyenLon y)
        {
            SoNguyenLon result = new SoNguyenLon();
            result.Number = Chia(x.Number, y.Number);
            return result;
        }

        public static SoNguyenLon operator %(SoNguyenLon x, SoNguyenLon y)
        {
            SoNguyenLon result = new SoNguyenLon();
            result.Number = ChiaLayDu(x.Number, y.Number);
            return result;
        }

        public static SoNguyenLon operator ^(SoNguyenLon x, SoNguyenLon y)
        {
            SoNguyenLon result = new SoNguyenLon();
            result.Number = LuyThua(x.Number, y.Number);
            return result;
        }
        #endregion

        #region Phep Tinh
        public static string Cong(string x, string y)
        {
            ThemSoKhong(ref x, ref y);
            string tong = "";
            int tam = 0;

            for (int i = x.Length - 1; i >= 0; i--)
            {
                tam = CharToInt(x[i]) + CharToInt(y[i]) + tam;
                tong = (tam % 10).ToString() + tong;
                tam = tam / 10;
            }

            if (tam > 0)
                tong = (tam % 10).ToString() + tong;

            return tong;
        }

        public static string Tru(string x, string y)
        {
            string so1;
            string so2;
            if (SoSanh(x, y) == 1)
            {
                so1 = x;
                so2 = y;
            }
            else
            {
                so1 = y;
                so2 = x;
            }

            ThemSoKhong(ref so1, ref so2);

            string hieu = "";
            int nho = 0;

            for (int i = so1.Length - 1; i >= 0; i--)
            {
                nho = so1[i] > so2[i] ? 0 : 10;

                hieu = (CharToInt(so1[i]) + nho - CharToInt(so2[i])).ToString() + hieu;

            }

            XoaSoKhong(ref hieu);

            if (SoSanh(x, y) == -1)
                hieu = '-' + hieu;
            return hieu;
        }

        public static string Nhan(string x, string y)
        {
            string tich = "0";
            string tam = "";

            for (int i = x.Length - 1; i >= 0; i--)
            {

                tam = NhanNho(CharToInt(x[i]), y);

                for (int j = 0; j < x.Length - i - 1; j++)
                    tam += "0";

                tich = Cong(tich, tam);
            }

            return tich;
        }

        public static string Chia(string x, string y, int phayDong)
        {
            string thuong = "";
            string du = "";

            for (int i = 0; i < x.Length; i++)
            {
                du += x[i];
                thuong += ChiaNho(ref du, y);
            }

            if (SoSanh(du, "0") == 1 && phayDong > 0)
            {
                thuong += ",";
                int i = 1;
                while (SoSanh(du, "0") != 0 && i <= phayDong)
                {
                    du += "0";
                    i++;
                    thuong += ChiaNho(ref du, y);
                }
            }

            XoaSoKhong(ref thuong);
            return thuong;
        }

        public static string Chia(string x, string y)
        {
            return Chia(x, y, 0);
        }

        public static string ChiaLayDu(string x, string y)
        {
            string du = "";
            for (int i = 0; i < x.Length; i++)
            {
                du += x[i];
                ChiaNho(ref du, y);
            }
            return du;
        }

        public static string LuyThua(string x, string n)
        {
            if (SoSanh(n, "0") == 0)
                return "1";
            else if (SoSanh(ChiaLayDu(n, "2"), "0") == 0)
            {
                string result = LuyThua(x, Chia(n, "2"));
                return Nhan(result, result);
            }
            else
            {
                string result = LuyThua(x, Chia(Tru(n, "1"), "2"));
                return Nhan(Nhan(x, result), result);
            }

        }

        public static string GiaiThua(int n)
        {
            string result = "1";
            for (int i = 2; i <= n; i++)
            {
                result = Nhan(i.ToString(), result);
            }
            return result;
        }

        #endregion

        #region So sanh
        public static int SoSanh(string x, string y)
        {
            int result = 0;
            if (x.Length == y.Length)
                result = x.CompareTo(y);
            else if (x.Length > y.Length)
                result = 1;
            else
                result = -1;
            return result;
        }

        public static int SoSanhSoThuc(string x, string y)
        {
            string[] so1 = x.Split(',');
            string[] so2 = y.Split(',');

            int result = SoSanh(so1[0], so2[0]);
            if (result == 0)
            {
                if (so1.Length > 1 && so2.Length > 1)
                {
                    ThemSoKhongSau(ref so1[1], ref so2[1]);
                    result = SoSanh(so1[1], so2[1]);
                }
                else
                {
                    if (so1.Length > 1)
                        result = 1;
                    if (so2.Length > 1)
                        result = -1;
                }
            }

            return result;
        }
        #endregion

        private static int CharToInt(char c)
        {
            return c - '0';
        }

        #region Chuan Hoa
        public static void ChuanHoa(ref string x, ref string y)
        {
            XoaSoKhong(ref x);
            XoaSoKhong(ref y);
            ThemDauCach(ref x, ref y);
        }

        public static void ChuanHoa(ref SoNguyenLon x, ref SoNguyenLon y)
        {
            string xx = x.ToString();
            string yy = y.ToString();
            XoaSoKhong(ref xx);
            XoaSoKhong(ref yy);
            ThemDauCach(ref xx, ref yy);
            x = xx;
            y = yy;
        }
        #endregion

        #region Phep toan bo tro cho nhan chia
        private static string ChiaNho(ref string du, string x)
        {
            string tam = "0";
            string thuong = "";
            XoaSoKhong(ref du);

            if (SoSanh(du, x) >= 0)
            {
                int j;

                for (j = 0; SoSanh(Cong(tam, x), du) < 1 && j < 10; j++)
                    tam = Cong(tam, x);

                thuong = j < 10 ? (thuong + j.ToString()) : "error";

                du = Tru(du, tam);
            }
            else
                thuong += "0";

            return thuong;
        }

        private static string NhanNho(int n, string x)
        {
            string tich = "";
            int tam = 0;

            for (int i = x.Length - 1; i >= 0; i--)
            {
                tam = n * CharToInt(x[i]) + tam;
                tich = (tam % 10).ToString() + tich;
                tam = tam / 10;
            }

            if (tam > 0)
                tich = (tam % 10).ToString() + tich;

            return tich;
        }

        #endregion

        #region Phuong thuc phu cho chuan hoa
        private static void ThemSoKhong(ref string x, ref string y)
        {
            while (x.Length > y.Length)
                y = "0" + y;
            while (x.Length < y.Length)
                x = "0" + x;
        }

        private static void ThemSoKhongSau(ref string x, ref string y)
        {
            while (x.Length > y.Length)
                y += "0";
            while (x.Length < y.Length)
                x += "0";
        }

        private static void ThemDauCach(ref string x, ref string y)
        {
            while (x.Length > y.Length)
                y = " " + y;
            while (x.Length < y.Length)
                x = " " + x;
        }

        private static void XoaSoKhong(ref string x)
        {
            while (x[0] == '0' && x.Length > 1)
                x = x.Remove(0, 1);
        }
        #endregion
    }
}
