# Опросы

## Отправка опроса

Минимальный набор: чат, вопрос и 2–10 вариантов ответа.

```csharp
var sent = await bot.SendPollAsync(
    new SendPollRequestParams(
        ChatId: chatId,
        Question: "Какой язык лучше для CLI-утилит?",
        Options: new[]
        {
            new InputPollOption("Go"),
            new InputPollOption("Rust"),
            new InputPollOption("C#"),
        }),
    CancellationToken.None);

// Сам опрос доступен через Message.Poll
Console.WriteLine($"Poll id: {sent.Poll!.Id}");
```

## Закрытие опроса

Опрос идентифицируется парой `chat_id + message_id`, а не `Poll.Id`. `stopPoll` возвращает финальный `Poll` со всей статистикой голосов.

```csharp
var closed = await bot.StopPollAsync(
    new StopPollRequestParams(sent.Chat.Id, sent.MessageId),
    CancellationToken.None);

foreach (var option in closed.Options)
    Console.WriteLine($"{option.Text}: {option.VoterCount}");
```

## Подписка на изменения голосов

Голоса в **неанонимных** опросах приходят как `Update.PollAnswer`. Для этого при `getUpdates` нужно явно добавить `poll_answer` в `allowed_updates` — по умолчанию Telegram его не присылает.

```csharp
var updates = await bot.GetUpdatesAsync(
    new GetUpdatesRequestParams(
        Offset: offset,
        Timeout: 30,
        AllowedUpdates: new[] { "message", "poll_answer" }),
    CancellationToken.None);

foreach (var update in updates)
{
    if (update.PollAnswer is not { } answer) continue;

    // Пустой OptionIds означает, что пользователь отозвал голос.
    var choice = answer.OptionIds.Count == 0
        ? "отозвал голос"
        : $"выбрал варианты [{string.Join(", ", answer.OptionIds)}]";

    Console.WriteLine($"Пользователь {answer.User?.Username ?? "?"} {choice}");
}
```

В анонимных опросах Telegram не раскрывает, кто как голосовал — обновляются только счётчики в самом объекте `Poll`.