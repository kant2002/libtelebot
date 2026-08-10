# Обычные клавиатуры, ForceReply и удаление

Помимо `InlineKeyboardMarkup` доступны ещё три варианта `reply_markup`, все реализуют `IReplyMarkup`.

## ReplyKeyboardMarkup

Обычная клавиатура заменяет клавиатуру устройства. Нажатие на кнопку отправляет её текст как сообщение.

```csharp
var keyboard = new ReplyKeyboardMarkup(
    Keyboard: new[]
    {
        new[] { new KeyboardButton("Меню"), new KeyboardButton("Помощь") },
        new[] { new KeyboardButton("Настройки") },
    },
    ResizeKeyboard: true,
    OneTimeKeyboard: true,
    InputFieldPlaceholder: "Выбери действие");

await bot.SendMessageAsync(
    new SendMessageRequestParams(chatId, "Главное меню", ReplyMarkup: keyboard),
    CancellationToken.None);
```

## ReplyKeyboardRemove

Убрать ранее показанную клавиатуру:

```csharp
await bot.SendMessageAsync(
    new SendMessageRequestParams(chatId, "Готово", ReplyMarkup: new ReplyKeyboardRemove()),
    CancellationToken.None);
```

## ForceReply

Автоматически открывает у пользователя интерфейс ответа на сообщение бота — удобно для пошаговых форм.

```csharp
await bot.SendMessageAsync(
    new SendMessageRequestParams(
        chatId,
        "Как тебя зовут?",
        ReplyMarkup: new ForceReply(InputFieldPlaceholder: "Имя")),
    CancellationToken.None);
```

Ответ пользователя придёт как обычное сообщение с заполненным `ReplyToMessage`, указывающим на вопрос бота — по нему можно связать ответ с формой.