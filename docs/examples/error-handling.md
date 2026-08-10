# Обработка ошибок

Все сбои сводятся к одному типу — `TelebotException`. Различить причину можно по полю `Code`:

- **HTTP-статус** (обычно 4xx / 5xx) — транспортный сбой ещё до Bot API.
- **`error_code` от Telegram** — прикладная ошибка (например, 400 «chat not found», 403 «bot was blocked»).
- **`null`** — нарушение протокола: пустое тело, `ok=true` без `result`, некорректный JSON.

## Простейший обработчик

```csharp
try
{
    await bot.SendMessageAsync(new SendMessageRequestParams(chatId, "hi"), CancellationToken.None);
}
catch (TelebotException ex)
{
    Console.WriteLine($"{ex.Code}: {ex.Message}");
}
```

## Обработка отдельных случаев

```csharp
try
{
    await bot.SendMessageAsync(new SendMessageRequestParams(chatId, "hi"), CancellationToken.None);
}
catch (TelebotException ex) when (ex.Code == 403)
{
    // Пользователь заблокировал бота — помечаем чат неактивным и не пробуем повторно.
    await MarkChatBlocked(chatId);
}
catch (TelebotException ex) when (ex.Code == 429)
{
    // Rate limit — Telegram сообщает retry_after в тексте ошибки.
    Console.WriteLine($"Слишком много запросов: {ex.Message}");
}
catch (TelebotException ex) when (ex.Code is null)
{
    // Протокольная ошибка — редкий, но фатальный сценарий.
    Console.Error.WriteLine($"Bot API вернул нечто странное: {ex.Message}");
}
```

## Отмена операции

Отмена через `CancellationToken` пробрасывает стандартный `OperationCanceledException`, а не `TelebotException`.

```csharp
using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));

try
{
    await bot.GetUpdatesAsync(new GetUpdatesRequestParams(Timeout: 30), cts.Token);
}
catch (OperationCanceledException)
{
    Console.WriteLine("Ждали слишком долго, отменили.");
}
```