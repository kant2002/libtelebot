# Ответы, цитаты и форматирование

## Ответ на сообщение

`ReplyParameters` — современная замена устаревшей паре `reply_to_message_id` + `allow_sending_without_reply`.

```csharp
await bot.SendMessageAsync(
    new SendMessageRequestParams(
        chatId,
        "Отвечаю на твоё сообщение",
        ReplyParameters: new ReplyParameters(MessageId: originalMessageId)),
    CancellationToken.None);
```

## Ответ с цитатой фрагмента

Можно подсветить конкретный кусок исходного сообщения. `QuotePosition` — смещение в UTF-16 code units (в .NET строки уже UTF-16, поэтому пересчёты не нужны).

```csharp
await bot.SendMessageAsync(
    new SendMessageRequestParams(
        chatId,
        "Речь про эту часть →",
        ReplyParameters: new ReplyParameters(
            MessageId: originalMessageId,
            Quote: "интересующий фрагмент",
            QuotePosition: 42)),
    CancellationToken.None);
```

## Ответ на сообщение из другого чата

`ChatId` в `ReplyParameters` позволяет сослаться на сообщение вне текущего чата — например, при пересылке.

```csharp
await bot.SendMessageAsync(
    new SendMessageRequestParams(
        chatId: myChatId,
        Text: "См. обсуждение в общем канале",
        ReplyParameters: new ReplyParameters(
            MessageId: publicMessageId,
            ChatId: publicChatId,
            AllowSendingWithoutReply: true)),
    CancellationToken.None);
```

## Разметка текста

```csharp
await bot.SendMessageAsync(
    new SendMessageRequestParams(
        chatId,
        "*Жирный*, _курсив_, `код` и [ссылка](https://example.com)",
        ParseMode: "MarkdownV2"),
    CancellationToken.None);
```

Возможные значения `ParseMode`: `MarkdownV2`, `HTML`, `Markdown` (устарел). Без указания — plain text.

## Управление превью ссылок

Превью пришли на смену устаревшему `disable_web_page_preview`.

```csharp
// Без превью:
await bot.SendMessageAsync(
    new SendMessageRequestParams(
        chatId,
        "https://example.com",
        LinkPreviewOptions: new LinkPreviewOptions(IsDisabled: true)),
    CancellationToken.None);

// Крупное превью над текстом:
await bot.SendMessageAsync(
    new SendMessageRequestParams(
        chatId,
        "Смотри статью: https://example.com/article",
        LinkPreviewOptions: new LinkPreviewOptions(
            PreferLargeMedia: true,
            ShowAboveText: true)),
    CancellationToken.None);
```