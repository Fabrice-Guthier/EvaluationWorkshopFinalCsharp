La BDD est sur le sql server de visual studio,
Il y a un data seeder pour peupler la BDD,
Les tests unitaires sont basés sur les méthodes des controllers ( ils sont tous au vert normalement ),
J'ai utilisé Entity pour gérer les données, c'est du code first via l'ORM

Il faudra quand même un petit " dotnet ef migrations add InitialCreate " , le scope dans program.cs fera l'update et le seeding de la bdd
