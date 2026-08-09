namespace Telebot;

/// <summary>
/// Базовый тип для описаний запросов к Telegram Bot API.
/// Хранит имя endpoint'а; наследники переопределяют <see cref="GetRequestFields"/>
/// и/или <see cref="GetRequestFiles"/>, если им есть что передать.
/// </summary>
/// <param name="Endpoint">Имя метода Bot API (например, <c>sendMessage</c>).</param>
public record TelegramRequest(string Endpoint) : ITelegramEncodable
{
    /// <inheritdoc />
    public virtual IEnumerable<TelegramRequestField> GetRequestFields()
    {
        return Enumerable.Empty<TelegramRequestField>();
    }

    /// <inheritdoc />
    public virtual IEnumerable<TelegramRequestFile> GetRequestFiles()
    {
        return Enumerable.Empty<TelegramRequestFile>();
    }
}