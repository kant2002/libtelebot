# Bucketlab.Telebot

[![NuGet](https://img.shields.io/nuget/v/Bucketlab.Telebot.svg)](https://www.nuget.org/packages/Bucketlab.Telebot)
[![Downloads](https://img.shields.io/nuget/dt/Bucketlab.Telebot.svg)](https://www.nuget.org/packages/Bucketlab.Telebot)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![.NET](https://img.shields.io/badge/.NET-9.0-512BD4)](https://dotnet.microsoft.com/)

Тонкий типобезопасный клиент Telegram Bot API для .NET 9. Никакой магии, никакого `dynamic`, никакого DI-контейнера — только `System.Net.Http` и `System.Text.Json` под капотом.

- Методы называются как в [официальной документации](https://core.telegram.org/bots/api) (`SendMessageAsync`, `EditMessageTextAsync`, …) — учить нечего.
- Все ошибки — сетевые, HTTP, `ok=false` от Telegram — сводятся к единому `TelebotException` с полем `Code`.
- Файлы отправляются по `file_id`, URL или потоком — транспорт сам выбирает form-urlencoded или multipart.

## Установка

```bash
dotnet add package Bucketlab.Telebot
```

## Эхо-бот

```csharp
using Telebot;

var bot = new Telegram("BOT_TOKEN");
var offset = 0;

while (true)
{
    var updates = await bot.GetUpdatesAsync(
        new GetUpdatesRequestParams(Offset: offset, Timeout: 30),
        CancellationToken.None);

    foreach (var update in updates)
    {
        offset = (int)update.UpdateId + 1;

        if (update.Message?.Text is not { } text) continue;

        await bot.SendMessageAsync(
            new SendMessageRequestParams(update.Message.Chat.Id, $"Echo: {text}"),
            CancellationToken.None);
    }
}
```

Всё, что нужно: токен от BotFather, цикл `getUpdates` с монотонным `offset`, отправка ответа.

## Что поддерживается

| Метод | Описание |
|---|---|
| `getMe` | информация о боте, проверка токена |
| `getUpdates` | long-polling новых событий |
| `setWebhook` | регистрация webhook-URL |
| `sendMessage` | отправка текстовых сообщений |
| `sendPhoto` | отправка фото (file_id / URL / поток) |
| `editMessageText` | редактирование текста ранее отправленного сообщения |
| `editMessageReplyMarkup` | замена или снятие инлайн-клавиатуры |
| `sendPoll` | отправка опросов |
| `stopPoll` | принудительное закрытие опроса с финальной статистикой |
| `answerCallbackQuery` | подтверждение приёма нажатия кнопки |
| `getFile` + скачивание | получение метаданных файла и его содержимого |

## Примеры

Всё, что сложнее эхо-бота, вынесено в [docs/examples/](docs/examples/):

- [Инлайн-клавиатуры и `callback_query`](docs/examples/inline-keyboards.md)
- [Обычные клавиатуры, `ForceReply` и их удаление](docs/examples/reply-keyboards.md)
- [Опросы и `poll_answer`-апдейты](docs/examples/polls.md)
- [Отправка фото](docs/examples/photos.md)
- [Скачивание файлов](docs/examples/files.md)
- [Webhook вместо long-polling](docs/examples/webhooks.md)
- [Ответы, цитаты и форматирование](docs/examples/replies-and-formatting.md)
- [Кастомный транспорт (self-hosted API, прокси, моки)](docs/examples/custom-transport.md)
- [Обработка ошибок](docs/examples/error-handling.md)

## Планы

Ближайшие направления развития — по мере запроса, без жёстких сроков:

- **Инлайн-режим.** Модель `InlineQuery`, поле в `Update`, `answerInlineQuery` с типом-результатом `InlineQueryResultArticle` для текстовых подсказок.
- **Медиа-методы.** `sendDocument`, `sendVideo`, `sendAudio` — по тому же контракту, что уже реализован для `sendPhoto`.
- **Форматирование в тексте.** `entities` в `sendMessage` / `editMessageText` (сейчас есть только `parse_mode`).
- **Бизнес-подключения.** `business_connection_id` в методах отправки и редактирования — когда появится реальный кейс с бизнес-аккаунтами.

Список не финальный — новые методы добавляются по мере появления реальных потребностей. Хочешь ускорить конкретный пункт — открывай issue с описанием сценария.

## Лицензия

MIT © Bucketlab