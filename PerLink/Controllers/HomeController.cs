using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PerLink.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index() 
        {
            
            return View();
        }


        [HttpPost]
        public ActionResult Verificar(string numero)
        {
            if (numero == null)
            {
                ViewBag.Resposta = "Favor informar um número válido";
                return View("Index");
            }

            ViewBag.Resposta = numero.ToString() + " -";

            //Verifica se o número é Sortudo
            if (VerificaSeESortudo(Int32.Parse(numero)))
            {
                ViewBag.Resposta = ViewBag.Resposta + " Número Sortudo";
            }
            else
            {
                ViewBag.Resposta = ViewBag.Resposta + " Número Não-Sortudo";
            }

            //Verifica se o número é Feliz
            if (VerificaSeEfeliz(Int32.Parse(numero)))
            {
                ViewBag.Resposta = ViewBag.Resposta + " e Feliz.";
            }
            else
            {
                ViewBag.Resposta = ViewBag.Resposta + " e Não-Feliz.";
            }



            return View("Index");
        }

        public static bool VerificaSeEfeliz(int numero)
        {
            bool numeroFeliz = false;
            List<int> listaDigitos = new List<int>();
            listaDigitos = DividirDigitos(numero);
            for (int i = 0; i < 100 && !numeroFeliz; i++)
            {
                int sumaActual = CalcularQuadrados(listaDigitos);
                if (sumaActual != 1)
                    listaDigitos = DividirDigitos(sumaActual);
                else numeroFeliz = true;
            }
            return numeroFeliz;
        }

        public static List<int> DividirDigitos(int digito)
        {
            List<int> digitos = new List<int>();
            while (digito != 0)
            {
                digitos.Add(digito % 10);
                digito = digito / 10;
            }
            return digitos;
        }

        public static int CalcularQuadrados(List<int> listaDigitos)
        {
            int resultado = 0;
            foreach (int elem in listaDigitos) resultado += elem * elem;
            return resultado;
        }       


        private static bool VerificaSeESortudo(long numero)
        {
            List<long> lista = new List<long>();

            for (int i = 1; i <= numero; i++)
            {
                lista.Add(i);
            }

            lista.RemoveAll(x => (x % 2 == 0));

            for (int i = 1; i < lista.Count; i++)
            {
                long multiplo = lista[i];

                int j = 1;

                while (true)
                {
                    var quadrado = Math.Pow(multiplo, j);

                    if (quadrado < lista.Count)
                    {
                        lista.RemoveAt(int.Parse(quadrado.ToString()) - 1);
                        j++;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return lista.Contains(numero);
        }


    }
}