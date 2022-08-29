using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml.Linq;

namespace NetDevTools.Core.Framework
{
    public static class MethodsExtensions
    {
        /// <summary>
        /// Aplica máscara a um texto
        /// </summary>
        /// <param name="textoformatar">Texto a ser formatado</param>
        /// <param name="mascara">Máscara utilizando cerquilha.</param>
        /// <returns>Texto com máscara aplicada</returns>
        public static string DevNetToolsSetMascara(this string textoformatar, string mascara)
        {
            if (string.IsNullOrEmpty(textoformatar)) textoformatar = string.Empty;

            string novoValor = string.Empty;
            int posicao = 0;
            for (int i = 0; mascara.Length > i; i++)
            {
                if (mascara[i] == '#')
                {
                    if (textoformatar.Length > posicao)
                    {
                        novoValor = novoValor + textoformatar[posicao];
                        posicao++;
                    }
                    else
                        break;
                }
                else
                {
                    if (textoformatar.Length > posicao)
                        novoValor = novoValor + mascara[i];
                    else
                        break;
                }
            }
            return novoValor;
        }
        public static bool DevNetToolsSomenteNumeros(this string texto)
        {
            try
            {
                for (int i = 0; i < texto.Length; i++)
                {
                    if (char.IsNumber(texto, i) == false) // se não é string
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Falha ao verificar se o texto: " + texto + " contém somente números.", ex);
            }
        }
        public static string DevNetToolsRemoveAcentos(this string text)
        {
            var sbReturn = new StringBuilder();
            var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();

            foreach (char letter in arrayText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                    sbReturn.Append(letter);
            }
            return sbReturn.ToString();
        }
        public static string DevNetToolsRemoveCaracteresEspeciais(this string text)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;

            string pattern = @"(?i)[^0-9a-záéíóúàèìòùâêîôûãõç\s]";
            var rgx = new Regex(pattern);
            return rgx.Replace(text, string.Empty);
        }
        public static string DevNetToolsRemoveEspacos(this string text)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;

            return text.TrimEnd().TrimStart();
        }
        public static Nullable<T> DevNetToolsToNullable<T>(this string s) where T : struct
        {
            Nullable<T> result = new Nullable<T>();
            try
            {
                if (!string.IsNullOrEmpty(s) && s.Trim().Length > 0)
                {
                    TypeConverter conv = TypeDescriptor.GetConverter(typeof(T));
                    result = (T)conv.ConvertFrom(s);
                }
            }
            catch { }
            return result;
        }
        public static long DevNetToolsToLong(this string s)
        {
            long result = new long();
            try
            {
                if (!string.IsNullOrEmpty(s) && s.Trim().Length > 0)
                {
                    TypeConverter conv = TypeDescriptor.GetConverter(typeof(long));
                    result = (long)conv.ConvertFrom(s);
                }
                else result = 0;
            }
            catch { }
            return result;
        }
        public static string DevNetToolsToStringSafe(this object obj)
        {
            if (obj == null)
                return String.Empty;

            var resultado = obj.ToString();

            // remove caracteres de escapebind
            resultado = resultado.Replace("\r", "");
            resultado = resultado.Replace("\n", "");

            return resultado;
        }
        /// <summary>
        /// garante 2 casas decimais e remove caracteres convertendo para string
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="qtdCasasDecimais"> Qtd casas decimais </param>
        /// <returns></returns>
        public static string DevNetToolsToDecimals(this decimal obj, int qtdCasasDecimais = 2)
        {
            var mascara = "";
            for (int i = 0; i < qtdCasasDecimais; i++)
            {
                mascara += "0";
            }
            mascara = $@"##.{mascara}";
            var resultado = obj.ToString(mascara).Replace(",", "").Replace(".", "");
            return resultado;
        }
        public static int DevNetToolsToInt(this string s)
        {
            if (String.IsNullOrEmpty(s))
                return 0;

            int valorRetornar = 0;
            int.TryParse(s, out valorRetornar);
            return valorRetornar;
        }
        public static bool DevNetToolsEhDateTime(this string s)
        {
            DateTime temp;
            return DateTime.TryParse(s, out temp);
        }
        public static int? DevNetToolsToNullableInt(this string s)
        {
            if (String.IsNullOrEmpty(s))
                return new int?();

            int valorretornar = 0;
            int.TryParse(s, out valorretornar);
            return valorretornar;
        }
        public static long? DevNetToolsToNullableLong(this string s)
        {
            if (String.IsNullOrEmpty(s))
                return new long?();

            long valorretornar = 0;
            long.TryParse(s, out valorretornar);
            return valorretornar;
        }
        public static decimal DevNetToolsToDecimal(this string s)
        {
            decimal result = new decimal();
            try
            {
                if (!string.IsNullOrEmpty(s) && s.Trim().Length > 0)
                {
                    TypeConverter conv = TypeDescriptor.GetConverter(typeof(decimal));
                    result = (decimal)conv.ConvertFrom(s);
                }
                else result = 0;
            }
            catch { }
            return result;
        }
        public static decimal DevNetToolsToDecimal(this decimal? s)
        {
            if (s.HasValue) return s.Value;

            return decimal.Zero;
        }
        public static double DevNetToolsToDouble(this string s)
        {
            double result = new double();
            try
            {
                if (!string.IsNullOrEmpty(s) && s.Trim().Length > 0)
                {
                    TypeConverter conv = TypeDescriptor.GetConverter(typeof(double));
                    result = (double)conv.ConvertFrom(s);
                }
                else result = 0;
            }
            catch { }
            return result;
        }
        public static DateTime DevNetToolsToDateTime(this string s)
        {
            DateTime result = new DateTime();

            if (String.IsNullOrEmpty(s))
                return result;

            try
            {
                if (!string.IsNullOrEmpty(s) && s.Trim().Length > 0)
                {
                    TypeConverter conv = TypeDescriptor.GetConverter(typeof(DateTime));
                    result = (DateTime)conv.ConvertFrom(s);
                }
                else result = new DateTime();
            }
            catch { }
            return result;
        }
        public static bool DevNetToolsToBool(this string s)
        {
            if (s == null)
                return false;

            if (s == "true")
                return true;

            if (s.ToLower() == "sim")
                return true;

            return false;
        }
        public static bool? DevNetToolsToBoolNullable(this string s)
        {
            if (String.IsNullOrEmpty(s))
                return new bool?();

            return s.DevNetToolsToBool();
        }
        public static string DevNetToolsToBoolString(this bool b)
        {
            if (b == true)
                return "Sim";
            else
                return "Não";
        }
        /// <summary>
        /// Retorna uma instância do objeto nula
        /// </summary>
        /// <param name="obj">Objeto alvo do método de extensão</param>
        /// <returns>Retorna própria instância do objeto ou uma nova instância com dados default</returns>
        public static T DevNetToolsToBind<T>(this T obj) where T : class, new()
        {
            if (obj != null)
                return obj;

            return new T();
        }
        /// <summary>
        /// Evita que uma string seja repassada como NULA
        /// </summary>
        /// <param name="texto"></param>
        /// <returns>String.Empty caso o texto seja nulo</returns>
        public static string DevNetToolsToBind(this string texto)
        {
            if (texto == null)
                return "";
            return texto;
        }
        /// <summary>
        /// Retorna uma instância do objeto nula
        /// </summary>
        /// <param name="obj">Objeto alvo do método de extensão</param>
        /// <returns>Retorna própria instância do objeto ou uma nova instância com dados default</returns>
        public static IList<T> DevNetToolsToBind<T>(this IList<T> obj) where T : class, new()
        {
            if (obj != null)
                return obj;

            return new List<T>();
        }
        /// <summary>
        /// Método de Extensão: Remove um caractere de uma string.
        /// </summary>
        /// <param name="s">String</param>
        /// <param name="caractereRemover">Caractere a se remover</param>
        /// <returns>Retorna a String</returns>
        public static string DevNetToolsDeletarCaractere(this string s, string caractereRemover)
        {
            var stringRemover = s;

            while (stringRemover.IndexOf(caractereRemover) > 0)
            {
                var indice = s.IndexOf(caractereRemover);
                stringRemover = stringRemover.Remove(indice, 1);
            }

            return stringRemover;
        }
        /// <summary>
        /// Método de Extensão: Retorna uma string só numericos.
        /// </summary>
        /// <param name="s">String</param>
        /// <returns>Retorna a String</returns>
        public static string DevNetToolsStringNumeros(this string s)
        {
            if (string.IsNullOrEmpty(s)) return string.Empty;

            return String.Join("", System.Text.RegularExpressions.Regex.Split(s, @"[^\d]"));
        }
        public static string DevNetToolsCopiarTextoAteCaractere(this string s, string caractereEncontrar)
        {
            var stringBase = s;

            var indiceCaractere = stringBase.IndexOf(caractereEncontrar);

            if (indiceCaractere <= 0)
                return String.Empty;

            var texto = s.Substring(0, indiceCaractere);

            return texto;
        }
        /// <summary>
        /// Remove todos os caracteres encontrados na lista de caracteres
        /// </summary>
        /// <param name="s"></param>
        /// <param name="caracteres"></param>
        /// <returns></returns>
        public static string DevNetToolsDeletarCaracteres(this string s, string[] caracteresRemover)
        {
            var stringVerificar = s;

            foreach (var caractere in caracteresRemover)
            {
                while (stringVerificar.IndexOf(caractere) > 0)
                {
                    var indice = stringVerificar.IndexOf(caractere);
                    stringVerificar = stringVerificar.Remove(indice, 1);
                }
            }

            return stringVerificar;
        }
        public static string DevNetToolsExtrairValorDaLinha(this string conteudoLinha, int de, int ate)
        {
            int inicio = de - 1;
            return conteudoLinha.Substring(inicio, ate - inicio);
        }
        /// <summary>
        /// Valida se a string informada é um código GTIN válido
        /// </summary>
        /// <param name="s">Codigo a ser validado</param>
        /// <returns></returns>
        public static bool DevNetToolsValidarCodigoEan(this string s)
        {
            try
            {
                //Tamanhos permitidos no GTIN = 8 / 12 / 13 / 14
                int[] GTINlength = { 8, 12, 13, 14 };
                int n, soma, resultado, base10;

                if (!GTINlength.Contains(s.Length))
                {
                    return false;
                }

                //Checa se todos os caracteres do GTIN são números
                for (int i = 0; i <= s.Length - 1; i++)
                {
                    if (!int.TryParse(s.ElementAt(i).ToString(), out n))
                    {
                        return false;
                    }
                }

                soma = 0;

                //Se for GTIN-13 multiplica todas as posições pares menos a última por 1 e as ímpares por 3. Nos outros tamanhos, faz o inverso
                if (s.Length == 13)
                {
                    for (int i = 0; i <= s.Length - 2; i++)
                    {
                        if (i % 2 == 0)
                        {
                            soma += (Convert.ToInt32(s.ElementAt(i).ToString()) * 1);
                        }
                        else
                        {
                            soma += (Convert.ToInt32(s.ElementAt(i).ToString()) * 3);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i <= s.Length - 2; i++)
                    {
                        if (i % 2 == 0)
                        {
                            soma = soma + Convert.ToInt32(s.ElementAt(i).ToString()) * 3;
                        }
                        else
                        {
                            soma = soma + Convert.ToInt32(s.ElementAt(i).ToString()) * 1;
                        }
                    }
                }

                base10 = soma;
                if (base10 % 10 != 0)
                {
                    while (base10 % 10 != 0)
                    {
                        base10 += 1;
                    }
                }

                resultado = base10 - soma;
                if (resultado != Convert.ToInt32(s.ElementAt(s.Length - 1).ToString()))
                {
                    return false;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool DevNetToolsEhDocumentoValido(this string s)
        {
            if (string.IsNullOrEmpty(s)) return true;

            if (s.DevNetToolsEhCnpj()) return true;

            return s.DevNetToolsEhCpf();
        }
        /// <summary>
        /// Valida se o documento informado é um CPF valido
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool DevNetToolsEhCpf(this string s)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            s = s.Trim();
            s = s.Replace(".", "").Replace("-", "");
            if (s.Length != 11)
                return false;
            tempCpf = s.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return s.EndsWith(digito);
        }
        /// <summary>
        /// Valida se o documento informado é um CNPJ valido
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool DevNetToolsEhCnpj(this string s)
        {
            if (string.IsNullOrEmpty(s)) return false;


            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            s = s.Trim();
            s = s.Replace(".", "").Replace("-", "").Replace("/", "");
            if (s.Length != 14)
                return false;
            tempCnpj = s.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return s.EndsWith(digito);
        }
        /// <summary>
        /// Retorna o valor do elemento informado, se não existir catch
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        public static string DevNetToolsRetornarValorElement(this XElement xml, string element)
        {
            return xml.Descendants().Where(x => x.Name.LocalName == element).First().Value;
        }
        /// <summary>
        /// Retorna o valor do atributo informado, se não existir catch
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public static string DevNetToolsRetornarValorAttribute(this XElement xml, string attribute)
        {
            return xml.Descendants().Where(x => x.Attribute("Id") != null).FirstOrDefault().Attribute("Id").Value;
        }
        public static string DevNetToolsQueryString<T>(this T obj)
        {
            try
            {
                var str = new StringBuilder();

                foreach (var propertie in obj.GetType().GetProperties().Where(x => x.GetValue(obj, null) != null))
                {
                    if (propertie.PropertyType.Namespace == "System.Collections.Generic")
                    {
                        foreach (var value in (IList)propertie.GetValue(obj, null))
                        {
                            str.Append($"&{propertie.Name}={HttpUtility.UrlEncode(value.ToString())}");
                        }
                    }
                    else
                    {
                        if (propertie.GetValue(obj, null).ToString().DevNetToolsEhDateTime())
                        {
                            str.Append($"&{propertie.Name}={HttpUtility.UrlEncode(propertie.GetValue(obj, null).ToString().DevNetToolsToDateTime().ToString("yyyy-MM-dd HH:mm:ss"))}");
                        }
                        else
                        {
                            str.Append($"&{propertie.Name}={HttpUtility.UrlEncode(propertie.GetValue(obj, null).ToString())}");
                        }
                    }
                }

                return str.ToString();
            }
            catch
            {
                return null;
            }
        }
        public static decimal DevNetToolsArredondar(this decimal valor, int casasDecimais)
        {
            var valorNovo = decimal.Round(valor, casasDecimais, MidpointRounding.AwayFromZero);
            var valorNovoStr = valorNovo.ToString("F" + casasDecimais, CultureInfo.CurrentCulture);
            return decimal.Parse(valorNovoStr);
        }
        public static decimal? DevNetToolsArredondar(this decimal? valor, int casasDecimais)
        {
            if (valor == null) return null;
            return DevNetToolsArredondar(valor.Value, casasDecimais);
        }
        public static decimal DevNetToolsArredondarParaBaixo(this decimal valor, int casasDecimais)
        {
            var divisor = (decimal)Math.Pow(10, casasDecimais);
            var dividendo = (long)Math.Truncate(divisor * valor);
            return dividendo / divisor;
        }
    }
}
