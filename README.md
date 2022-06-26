<h2>HeroesAPI</h2>
  <p>Web API  является посредником между базой данных и клиентом, который может манипулировать сведениями о персонаже игры. </p>
  <p>Интерфейс построен на трех сущностях - персонаж, его профессия, а так же список прирученных ездовых животных.</p>
  <p>Структуру проекта разбил на несколько слоёв, согласно схеме чистой архитектуры, разделив пользовательский интерфейс, логику приложения и уровень доступа к БД друг от друга. Это решение добавляет гибкости, обусловленной простотой замены web-интерфейса и поставщика базы данных, а также упрощает тестирование компонентов.</p>
  <p>При написании логики реализовал шаблон CQRS в связке с библиотекой MediatR. Для валидации данных, пришедших в запросе, использую библиотеку FluentValidation.</p>
  <p>Написал unit-тесты для команд и запросов с использование xUnit и Moq.</p>
  <p>Так же для ограничения доступа к CRUD-командам, добавил регистрацию и аутентификацию с получением токена JWT. Для некоторых команд требуется роль администратора.</p> 
