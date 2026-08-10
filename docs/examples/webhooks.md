# Webhook вместо long-polling

Вместо периодического опроса через `getUpdates` можно зарегистрировать URL — Telegram будет сам POST-ить апдейты на него.

## Регистрация webhook

```csharp
await bot.SetWebhookAsync(
    new SetWebhookRequestParams(
        Url: "https://my-bot.example.com/telegram",
        AllowedUpdates: new[] { "message", "callback_query" },
        DropPendingUpdates: true,
        SecretToken: "my-shared-secret"),
    CancellationToken.None);
```

`SecretToken` Telegram будет присылать в заголовке `X-Telegram-Bot-Api-Secret-Token` — простой способ проверить, что входящий запрос действительно от Telegram, а не от постороннего.

## С самоподписанным сертификатом

Для внутренних инсталляций можно отдать публичную часть сертификата — Telegram будет использовать её при TLS-подключении.

```csharp
await using var cert = System.IO.File.OpenRead("public.pem");

await bot.SetWebhookAsync(
    new SetWebhookRequestParams(
        Url: "https://internal.example.com/telegram",
        Certificate: new InputFileWithStream(cert, "application/x-pem-file", "public.pem")),
    CancellationToken.None);
```

## Валидация входящего запроса на своём сервере

Псевдокод обработчика на стороне бэкенда:

```csharp
app.MapPost("/telegram", async (HttpRequest req) =>
{
    var secret = req.Headers["X-Telegram-Bot-Api-Secret-Token"].ToString();
    if (secret != Environment.GetEnvironmentVariable("TELEGRAM_SECRET"))
        return Results.Unauthorized();

    var update = await req.ReadFromJsonAsync<Update>();
    // …обрабатываем update…
    return Results.Ok();
});
```

Использовать одновременно `setWebhook` и `getUpdates` нельзя — Bot API не позволит.