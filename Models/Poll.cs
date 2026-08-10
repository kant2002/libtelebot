using System.Text.Json.Serialization;

namespace Telebot.Models;

/// <summary>
/// Вариант ответа в опросе (<see href="https://core.telegram.org/bots/api#polloption"/>).
/// </summary>
/// <param name="Text">Текст варианта, 1–100 символов.</param>
/// <param name="VoterCount">Число голосов за этот вариант.</param>
/// <param name="TextEntities">Спецсущности в <see cref="Text"/> (форматирование, кастомные эмодзи).</param>
public record PollOption(
    [property: JsonPropertyName("text")]
    string Text,

    [property: JsonPropertyName("voter_count")]
    int VoterCount,

    [property: JsonPropertyName("text_entities")]
    MessageEntity[]? TextEntities = null
);

/// <summary>
/// Опрос в чате (<see href="https://core.telegram.org/bots/api#poll"/>).
/// </summary>
/// <remarks>
/// Один тип модели описывает и обычный опрос (<c>type = "regular"</c>), и викторину
/// (<c>type = "quiz"</c>). Поля <see cref="CorrectOptionId"/>, <see cref="Explanation"/>,
/// <see cref="ExplanationEntities"/> — только для викторин.
/// <para/>
/// <see cref="CorrectOptionId"/> виден боту-автору всегда, остальным клиентам —
/// только после закрытия опроса.
/// </remarks>
/// <param name="Id">Уникальный идентификатор опроса.</param>
/// <param name="Question">Текст вопроса, 1–300 символов.</param>
/// <param name="Options">Варианты ответа (2–10 элементов).</param>
/// <param name="TotalVoterCount">Общее число уникальных проголосовавших.</param>
/// <param name="IsClosed"><c>true</c>, если опрос закрыт.</param>
/// <param name="IsAnonymous">
/// <c>true</c>, если опрос анонимный. Для анонимных опросов Telegram не присылает <c>poll_answer</c>.
/// </param>
/// <param name="Type"><c>"regular"</c> или <c>"quiz"</c>.</param>
/// <param name="AllowsMultipleAnswers">Разрешён ли множественный выбор. Всегда <c>false</c> для викторин.</param>
/// <param name="QuestionEntities">Спецсущности в тексте вопроса.</param>
/// <param name="CorrectOptionId">Индекс правильного варианта (0-based) — только для викторин.</param>
/// <param name="Explanation">Пояснение после ответа в викторине, 0–200 символов.</param>
/// <param name="ExplanationEntities">Спецсущности в <see cref="Explanation"/>.</param>
/// <param name="OpenPeriod">Время активности опроса в секундах.</param>
/// <param name="CloseDate">Абсолютное время автозакрытия (Unix-timestamp). Взаимоисключающе с <see cref="OpenPeriod"/>.</param>
public record Poll(
    [property: JsonPropertyName("id")]
    string Id,

    [property: JsonPropertyName("question")]
    string Question,

    [property: JsonPropertyName("options")]
    IReadOnlyList<PollOption> Options,

    [property: JsonPropertyName("total_voter_count")]
    int TotalVoterCount,

    [property: JsonPropertyName("is_closed")]
    bool IsClosed,

    [property: JsonPropertyName("is_anonymous")]
    bool IsAnonymous,

    [property: JsonPropertyName("type")]
    string Type,

    [property: JsonPropertyName("allows_multiple_answers")]
    bool AllowsMultipleAnswers,

    [property: JsonPropertyName("question_entities")]
    MessageEntity[]? QuestionEntities = null,

    [property: JsonPropertyName("correct_option_id")]
    int? CorrectOptionId = null,

    [property: JsonPropertyName("explanation")]
    string? Explanation = null,

    [property: JsonPropertyName("explanation_entities")]
    MessageEntity[]? ExplanationEntities = null,

    [property: JsonPropertyName("open_period")]
    int? OpenPeriod = null,

    [property: JsonPropertyName("close_date")]
    long? CloseDate = null
);

/// <summary>
/// Изменение голоса пользователя в неанонимном опросе
/// (<see href="https://core.telegram.org/bots/api#pollanswer"/>).
/// </summary>
/// <remarks>
/// Приходит в <see cref="Update.PollAnswer"/>. Только для неанонимных опросов.
/// Пустой <see cref="OptionIds"/> — пользователь отозвал голос.
/// Заполняется ровно одно из <see cref="User"/> и <see cref="VoterChat"/>.
/// </remarks>
/// <param name="PollId">Идентификатор опроса (совпадает с <see cref="Poll.Id"/>).</param>
/// <param name="OptionIds">Индексы выбранных вариантов (0-based).</param>
/// <param name="User">Проголосовавший пользователь.</param>
/// <param name="VoterChat">Чат-голосующий (анонимный админ канала/группы).</param>
public record PollAnswer(
    [property: JsonPropertyName("poll_id")]
    string PollId,

    [property: JsonPropertyName("option_ids")]
    IReadOnlyList<int> OptionIds,

    [property: JsonPropertyName("user")]
    User? User = null,

    [property: JsonPropertyName("voter_chat")]
    Chat? VoterChat = null
);