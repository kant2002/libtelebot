# Отправка фото

Фото задаётся одним из трёх способов через `InputFile`. Транспорт сам выбирает form-urlencoded или multipart в зависимости от типа.

## По URL

Telegram сам скачает файл по ссылке.

```csharp
await bot.SendPhotoAsync(
    new SendPhotoRequestParams(
        chatId,
        new InputFileWithUrl(new Uri("https://example.com/pic.jpg")),
        Caption: "Скачано по ссылке"),
    CancellationToken.None);
```

## Потоком

Файл читается на клиенте и отправляется через multipart/form-data.

```csharp
await using var file = System.IO.File.OpenRead("photo.jpg");

await bot.SendPhotoAsync(
    new SendPhotoRequestParams(
        chatId,
        new InputFileWithStream(file, ContentType: "image/jpeg", FileName: "photo.jpg")),
    CancellationToken.None);
```

## По file_id

Если файл уже был загружен на серверы Telegram (например, пришёл в апдейте), его можно переотправить, не выкачивая.

```csharp
await bot.SendPhotoAsync(
    new SendPhotoRequestParams(chatId, new InputFileWithId("AgACAgIAAxk...")),
    CancellationToken.None);
```

`file_id` действителен только в контексте того же бота — токен другого бота его не поймёт.

## Подпись с форматированием и спойлером

```csharp
await bot.SendPhotoAsync(
    new SendPhotoRequestParams(
        chatId,
        new InputFileWithUrl(new Uri("https://example.com/spoiler.jpg")),
        Caption: "*Осторожно, спойлер!*",
        ParseMode: "MarkdownV2",
        HasSpoiler: true),
    CancellationToken.None);
```