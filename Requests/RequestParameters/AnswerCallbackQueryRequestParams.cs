namespace Telebot;

/// <summary>
/// Параметры <c>answerCallbackQuery</c> — подтверждение приёма callback-запроса.
/// </summary>
/// <remarks>
/// Вызов обязателен: без него у пользователя на нажатой кнопке продолжает крутиться индикатор.
/// </remarks>
/// <param name="CallbackQueryId">Идентификатор из <see cref="Telebot.Models.CallbackQuery.Id"/>.</param>
/// <param name="Text">Текст уведомления, 0–200 символов. <c>null</c> — молча погасить индикатор.</param>
/// <param name="ShowAlert">Показать модальное окно вместо тоста.</param>
/// <param name="Url">
/// URL, который откроется у пользователя (игровые кнопки или редирект в бота через <c>t.me/&lt;bot&gt;?start=…</c>).
/// </param>
/// <param name="CacheTime">Время кеширования результата в секундах. По умолчанию 0.</param>
public sealed record AnswerCallbackQueryRequestParams(
    string CallbackQueryId,
    string? Text = null,
    bool? ShowAlert = null,
    string? Url = null,
    int? CacheTime = null
) : TelegramRequest("answerCallbackQuery")
{
    /// <inheritdoc />
    public override IEnumerable<TelegramRequestField> GetRequestFields()
    {
        yield return new TelegramRequestField("callback_query_id", CallbackQueryId);

        if (Text is not null)
            yield return new TelegramRequestField("text", Text);
        if (ShowAlert is not null)
            yield return new TelegramRequestField("show_alert",
                ShowAlert.Value ? "true" : "false");
        if (Url is not null)
            yield return new TelegramRequestField("url", Url);
        if (CacheTime is not null)
            yield return new TelegramRequestField("cache_time", CacheTime.Value.ToString());
    }
}