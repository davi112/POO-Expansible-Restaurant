// Desabilita a execução paralela dos testes para evitar conflitos em testes 
// que compartilham estado global
[assembly: CollectionBehavior(DisableTestParallelization = true)]
