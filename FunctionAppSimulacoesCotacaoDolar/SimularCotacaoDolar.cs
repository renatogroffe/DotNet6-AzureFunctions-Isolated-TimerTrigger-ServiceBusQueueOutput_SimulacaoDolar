using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using FunctionAppSimulacoesCotacaoDolar.Models;

namespace FunctionAppSimulacoesCotacaoDolar;

public static class SimularCotacaoDolar
{
    private const decimal VALOR_BASE = 4.68m;

    [Function(nameof(SimularCotacaoDolar))]
    [ServiceBusOutput("queue-dolar", Connection = "AzureServiceBusConnection")]
    public static DadosCotacao Run([TimerTrigger("*/5 * * * * *")] FunctionContext context)
    {
        var logger = context.GetLogger(nameof(SimularCotacaoDolar));

        var cotacao = new DadosCotacao()
        {
            Sigla = "USD",
            Horario = DateTime.Now,
            Valor = Math.Round(VALOR_BASE + new Random().Next(0, 21) / 1000m, 3)
        };
        logger.LogInformation($"Dados gerados: {JsonSerializer.Serialize(cotacao)}");

        return cotacao;
    }
}