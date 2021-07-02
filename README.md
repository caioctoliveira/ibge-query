# CaioOliveira.IBGE

Essa biblioteca tem por objetivo realizar consultas nas APIs do IBGE.

Nesta primeira versão estão implementadas apenas consultas de estado e cidade, por serem os mais utilizados.

Toda e qualquer contribuição é bem vinda, envie seu Pull Request :smiley:

Para utlizar a biblioteca é muito simples, basta adicionar a linha abaixo no startup da sua aplicação:

O código abaixo representa como utilizar com configuração padrão

```c#
public void ConfigureServices(IServiceCollection services)
{
    ...
    services.UseIbgeService();
    ...    
}
```

O código abaixo representa como utilizar com suas configurações

```c#
public void ConfigureServices(IServiceCollection services)
{
    ...
    services.UseIbgeService(opt => 
    {
        opt.BaseApiUrl = "http://urlnova.com.br"
    });
    ...    
}
```

Esta possibilidade existe para o caso do IBGE resolver mudar a url base, desde que não exista quebra do contrato, obviamente. Uma funcionalidade meio inútil porém nos dá essa flexibilidade caso um cenário desse ocorra.

Pronto, agora que configuramos o serviço basta solicitar o serviço em qualquer lugar de sua aplicação, conforme exemplo abaixo:

```c#
public class FooAppService
{
    private readonly IIbgeService _ibgeService;

    public FooAppService(IIbgeService ibgeService)
    {
        _ibgeService = ibgeService;   
    }
    
    public async Task GetCity()
    {
        ...
        //Recupera a cidade com o Código IBGE (35 é São Paulo)
        var city = await _ibgeService.GetCity(35);
        ...
    }
}
```

