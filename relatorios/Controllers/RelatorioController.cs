using Microsoft.AspNetCore.Mvc;
using PdfSharpCore.Drawing;
using relatorios.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace relatorios.Controllers
{
    public class RelatorioController : Controller
    {
        private readonly relatoriosContext _context;

        public RelatorioController(relatoriosContext context)
        {
            _context = context;
        }

        public IActionResult Relatorio()
        {
            return View();
        }

        public FileResult PdfRelatorioSharpCore()
        {
            var relat = _context.Teste.ToList().OrderBy(a => a.Id);
            var height = 0;
            var contagem = 0;
            var doc = new PdfSharpCore.Pdf.PdfDocument(); //instancia do obj PdfSharpCore

            var page = doc.AddPage(); //adiciona pagina.
            page.Size = PdfSharpCore.PageSize.A4; //define o tipo da pagina
            page.Orientation = PdfSharpCore.PageOrientation.Portrait; //orientação da pagina em retrato.
            var graphics = PdfSharpCore.Drawing.XGraphics.FromPdfPage(page); //page em gráficos
            var corFonte = PdfSharpCore.Drawing.XBrushes.Gray; //cor da fonte

            var textFormatter = new PdfSharpCore.Drawing.Layout.XTextFormatter(graphics); //formato de texto, responsável pela impressão
            var fonteOrganizada = new PdfSharpCore.Drawing.XFont("Arial", 10); //definições da font (xfont("tipo", "tamanho"))
            var fonteDescricao = new PdfSharpCore.Drawing.XFont("Arial", 8, PdfSharpCore.Drawing.XFontStyle.BoldItalic);
            var subtitulodetalhes = new PdfSharpCore.Drawing.XFont("Arial", 12, PdfSharpCore.Drawing.XFontStyle.Bold);
            var titulodetalhes = new PdfSharpCore.Drawing.XFont("Arial", 16, PdfSharpCore.Drawing.XFontStyle.Bold);
            var fonteDetalheDescricao = new PdfSharpCore.Drawing.XFont("Arial", 7);

            var logo = @"C:\img\Logo-Include.png";

            var qtdPaginas = doc.PageCount; //contador de paginas
            var tituloDetalhes = new PdfSharpCore.Drawing.Layout.XTextFormatter(graphics); //configurações de titulo

            
            textFormatter.DrawString(qtdPaginas.ToString(), new PdfSharpCore.Drawing.XFont("Arial", 10), corFonte, new PdfSharpCore.Drawing.XRect(500, 1000, page.Width, page.Height)); //imprimir quantidade de paginas no cabeçalho centralizando (xrect)

            XImage imagem = XImage.FromFile(logo); //leitura da imagem
            graphics.DrawImage(imagem, 20, 5, 200, 100); //posição da imagem, width e height

            tituloDetalhes.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
            tituloDetalhes.DrawString("Data: 00/00/0000", titulodetalhes, corFonte, new PdfSharpCore.Drawing.XRect(30, 120, page.Width, page.Height));
            tituloDetalhes.DrawString("Gerado em: 00/00/0000", titulodetalhes, corFonte, new PdfSharpCore.Drawing.XRect(250, 120, page.Width, page.Height));
            //graphics.DrawRoundedRectangle(PdfSharpCore.Drawing.XPens.Transparent, PdfSharpCore.Drawing.XBrushes.Silver, 0, 100, 1000, 1, 1, 1); //desenha uma linha.

            textFormatter.DrawString("Id", subtitulodetalhes, corFonte, new PdfSharpCore.Drawing.XRect(25, 170, page.Width, page.Height)); //escreva nome no relatorio na posição indicada.
            textFormatter.DrawString("Nome", subtitulodetalhes, corFonte, new PdfSharpCore.Drawing.XRect(125, 170, page.Width, page.Height)); //escreva nome no relatorio na posição indicada.
            textFormatter.DrawString("Idade", subtitulodetalhes, corFonte, new PdfSharpCore.Drawing.XRect(200, 170, page.Width, page.Height)); //escreva nome no relatorio na posição indicada.
            textFormatter.DrawString("Apelido", subtitulodetalhes, corFonte, new PdfSharpCore.Drawing.XRect(275, 170, page.Width, page.Height)); //escreva nome no relatorio na posição indicada.

            foreach (var item in relat)
            {
                if (contagem % 2 == 0) //POG
                {
                    graphics.DrawRoundedRectangle(PdfSharpCore.Drawing.XPens.Silver, PdfSharpCore.Drawing.XBrushes.Silver, 5, 205 + height, 580, 20, 10, 0); //desenha uma linha. (Margin-Left | Margin-Top | Width | Height | bordas)
                } //linha recebe o preenchimento se a contagem for par

                contagem += 1;
                textFormatter.DrawString(item.Id.ToString(), subtitulodetalhes, corFonte, new PdfSharpCore.Drawing.XRect(25, 210 + height, page.Width, page.Height)); //escreva nome no relatorio na posição indicada.
                textFormatter.DrawString(item.Nome.ToString(), fonteDescricao, corFonte, new PdfSharpCore.Drawing.XRect(125, 210 + height, page.Width, page.Height)); //escreva nome no relatorio na posição indicada.
                textFormatter.DrawString(item.Idade.ToString(), fonteDescricao, corFonte, new PdfSharpCore.Drawing.XRect(200, 210 + height, page.Width, page.Height)); //escreva nome no relatorio na posição indicada.
                textFormatter.DrawString(item.Apelido ?? "Vazio", fonteDescricao, corFonte, new PdfSharpCore.Drawing.XRect(275, 210 + height, page.Width, page.Height)); //escreva nome no relatorio na posição indicada.
                height += 20;
            }

            MemoryStream stream = new MemoryStream(); //instancia de memoryStream
            var contenteType = "application/pdf"; //define o formato do relatório em PDF
            doc.Save(stream, false); //não fechar após salvar
            var nomeArquivo = "RelatorioNome.pdf";

            return File(stream.ToArray(), contenteType, nomeArquivo);
        }


    }
}
