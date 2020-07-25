Imports BoletoNet

Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'GerarBoleto(New Date(2014, 7, 16), 1617.16, "1403141309")
            'GerarBoleto(New Date(2014, 7, 16), 2, "1")
            GerarBoleto(New Date(2014, 7, 16), 80, "1403141309")
            'GerarBoleto(New Date(1997, 10, 7), 80, "1403141309")
        End If
    End Sub

    Sub GerarBoleto(Vencimento As Date, Valor As Double, NossoNumero As String)
        Dim c As New Cedente
        c.Nome = ""
        c.CPFCNPJ = "00000000000000"
        c.Convenio = 2366721
        c.ContaBancaria = New ContaBancaria()

        Dim s As New Sacado With {.Nome = "Cliente", .Endereco = New Endereco}

        Dim b As New Boleto With {.Carteira = "18-019", .Cedente = c, .Sacado = s}
        b.DataVencimento = Vencimento
        b.ValorBoleto = Valor
        b.NossoNumero = NossoNumero

        For i As Integer = 1 To 7 '7 linhas é o espaço que +ou- o boleto real tem
            b.Instrucoes.Add(New Instrucao_BancoBrasil() With {.Descricao = String.Empty})
        Next

        Dim bb As New BoletoBancario() With {.CodigoBanco = 1, .Boleto = b, .MostrarComprovanteEntrega = False}
        bb.Boleto.Valida()

        If panelBoleto.Controls.Count = 0 Then
            panelBoleto.Controls.Add(bb)
        End If
    End Sub

End Class
