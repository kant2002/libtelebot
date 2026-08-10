# Инлайн-клавиатуры и callback_query

Инлайн-клавиатура крепится к сообщению. Кнопки могут вести на URL или отправлять боту `callback_data` при нажатии.

## Отправка сообщения с клавиатурой

`InlineKeyboard` — двумерный массив: внешний уровень задаёт ряды, внутренний — кнопки в ряду.

```csharp
using Telebot;

var keyboard = new InlineKeyboardMarkup(new[]
{
    new[]
    {
        new InlineKeyboardButton("A", CallbackData: "choice:a"),
        new InlineKeyboardButton("B", CallbackData: "choice:b"),
    },
    new[] { new InlineKeyboardButton("C", CallbackData: "choice:c") },
    new[] { new InlineKeyboardButton("Открыть сайт", Url: "https://example.com") },
});

await bot.SendMessageAsync(
    new SendMessageRequestParams(chatId, "Выбери вариант", ReplyMarkup: keyboard),
    CancellationToken.None);
```

## Обработка нажатия

Нажатие приходит в `Update.CallbackQuery`. Ответ через `answerCallbackQuery` обязателен — без него у пользователя на кнопке остаётся вертящийся индикатор.

```csharp
foreach (var update in updates)
{
    if (update.CallbackQuery is not { } cb) continue;

    // 1. Гасим индикатор на кнопке (обязательно).
    await bot.AnswerCallbackQueryAsync(
        new AnswerCallbackQueryRequestParams(cb.Id, Text: "Принято"),
        CancellationToken.None);

    // 2. Обновляем исходное сообщение.
    if (cb.Message is { } msg)
    {
        await bot.EditMessageTextAsync(
            new EditMessageTextRequestParams(msg.Chat.Id, msg.MessageId, $"Ты выбрал: {cb.Data}"),
            CancellationToken.None);
    }
}
```

## Модальное окно вместо тоста

`ShowAlert: true` — покажет модалку, которую нужно закрыть тапом.

```csharp
await bot.AnswerCallbackQueryAsync(
    new AnswerCallbackQueryRequestParams(cb.Id, Text: "Требуется подтверждение", ShowAlert: true),
    CancellationToken.None);
```

## Снятие клавиатуры

Заменить или снять клавиатуру, не трогая текст:

```csharp
await bot.EditMessageReplyMarkupAsync(
    new EditMessageReplyMarkupRequestParams(chatId, messageId, ReplyMarkup: null),
    CancellationToken.None);
```