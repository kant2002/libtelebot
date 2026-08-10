# Скачивание файлов

Получение файла — двухшаговая операция: сначала `getFile` возвращает метаданные и путь, потом отдельный HTTP-запрос выкачивает содержимое.

## Метаданные

```csharp
var file = await bot.GetFileAsync(
    new GetFileRequestParams(FileId: "AgACAgIAAxk..."),
    CancellationToken.None);

Console.WriteLine($"Размер: {file.FileSize} байт, путь: {file.FilePath}");
```

Ссылка для скачивания живёт минимум час. Если протухла — повторный вызов `getFile` с тем же `FileId`.

## Скачивание в файл

`DownloadFileAsync` возвращает `Stream` над HTTP-соединением — читай его напрямую в целевой файл, не буферизуя в памяти. Вызывающий обязан закрыть поток.

```csharp
var file = await bot.GetFileAsync(new GetFileRequestParams(fileId), CancellationToken.None);

await using var remote = await bot.DownloadFileAsync(file, CancellationToken.None);
await using var local = System.IO.File.Create("downloaded.jpg");

await remote.CopyToAsync(local, CancellationToken.None);
```

## Скачивание в память

Если файл маленький и нужен как `byte[]`:

```csharp
await using var remote = await bot.DownloadFileAsync(file, CancellationToken.None);
using var memory = new MemoryStream();

await remote.CopyToAsync(memory, CancellationToken.None);
var bytes = memory.ToArray();
```

## Лимиты и self-hosted API

Через публичный `api.telegram.org` — максимум 20 MB на скачивание. Для файлов крупнее нужен [self-hosted Bot API server](https://github.com/tdlib/telegram-bot-api) — там лимит до 2 GB, и `FilePath` может прийти как абсолютный локальный путь на диске сервера, доступный без HTTP-запроса.