# Примеры использования

Каждый файл — отдельный сценарий. Тривиальный эхо-бот лежит в корневом [README](../../README.md), здесь — всё, что чуть сложнее одного вызова `SendMessageAsync`.

| Файл | Что показывает |
|---|---|
| [inline-keyboards.md](inline-keyboards.md) | Инлайн-клавиатура и обработка нажатий через `callback_query` |
| [reply-keyboards.md](reply-keyboards.md) | Обычная клавиатура, её удаление и `force_reply` |
| [polls.md](polls.md) | Отправка опроса, закрытие опроса, `poll_answer`-апдейты |
| [photos.md](photos.md) | Отправка фото по `file_id`, URL и потоку |
| [files.md](files.md) | Получение метаданных файла и скачивание бинарного содержимого |
| [webhooks.md](webhooks.md) | Регистрация webhook-URL вместо long-polling |
| [custom-transport.md](custom-transport.md) | Self-hosted Bot API, свой таймаут, свой `ITelegramTransport` |
| [error-handling.md](error-handling.md) | `TelebotException` и различение источников ошибок |
| [replies-and-formatting.md](replies-and-formatting.md) | Ответы на сообщения, цитаты, `parse_mode`, превью ссылок |