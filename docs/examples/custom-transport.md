# Кастомный транспорт

По умолчанию клиент ходит в `https://api.telegram.org` с таймаутом 30 секунд. Оба параметра переопределяются через `DefaultTransportOptions`.

## Свой адрес и таймаут

Типовой сценарий — self-hosted [Bot API server](https://github.com/tdlib/telegram-bot-api), у которого лимиты выше и имеет смысл поднять таймаут.

```csharp
var transport = new DefaultTelegramTransport(new DefaultTransportOptions
{
    BaseAddress = new Uri("https://my-selfhosted-bot-api.local"),
    Timeout = TimeSpan.FromSeconds(60),
});

var bot = new Telegram(transport, "BOT_TOKEN");
```

## Один транспорт на процесс

`DefaultTelegramTransport` — долгоживущий сервис. Держи один инстанс на всё приложение и переиспользуй. Пересоздание на каждый запрос ведёт к утечке сокетов (классическая грабля `HttpClient` в .NET, не специфика библиотеки).

## Свой ITelegramTransport

Для прокси, своего `HttpMessageHandler`, логирования или ретраев — реализуй интерфейс целиком, а не расширяй `DefaultTransportOptions` (он специально минимальный).

```csharp
public sealed class LoggingTransport(ITelegramTransport inner, ILogger logger) : ITelegramTransport
{
    public async Task<T> RequestAsync<T>(TelegramRequest request, string token, CancellationToken ct)
    {
        var sw = System.Diagnostics.Stopwatch.StartNew();
        try
        {
            var result = await inner.RequestAsync<T>(request, token, ct);
            logger.LogInformation("{Endpoint} OK за {Ms} мс", request.Endpoint, sw.ElapsedMilliseconds);
            return result;
        }
        catch (TelebotException ex)
        {
            logger.LogWarning(ex, "{Endpoint} FAIL {Code}", request.Endpoint, ex.Code);
            throw;
        }
    }

    public Task<Stream> DownloadAsync(Telebot.Models.File file, string token, CancellationToken ct)
        => inner.DownloadAsync(file, token, ct);
}

var bot = new Telegram(
    new LoggingTransport(new DefaultTelegramTransport(), logger),
    "BOT_TOKEN");
```

## Мок в тестах

`ITelegramTransport` подменяется любым моком без сети — проверяй, что запрос собран правильно и результат разобран корректно.

```csharp
public sealed class FakeTransport : ITelegramTransport
{
    public Task<T> RequestAsync<T>(TelegramRequest request, string token, CancellationToken ct)
        => Task.FromResult((T)(object)new User(1, IsBot: true, FirstName: "Fake"));

    public Task<Stream> DownloadAsync(Telebot.Models.File file, string token, CancellationToken ct)
        => throw new NotImplementedException();
}

var bot = new Telegram(new FakeTransport(), "any-token");
var me = await bot.GetMeAsync(new GetMeRequestParams(), CancellationToken.None);
```