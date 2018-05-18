# Desafio Mundipagg

O Desafio consiste em criar uma engine de transformação de mensagens para um sistema de censo.
Cada estado tem uma entrada diferente, a engine precisará se adequar pra cada tipo deentrada e retornar uma única saida. 
A API deverá aceitar templates e executa-los de forma dinamica.


## Getting Started

A API foi desenvolvida em .net Core 2.0

IDE Utilizada: Visual Studio 2017 CODE

Testes Unitarios: nUnit 3.1

Testes de caixa branca: SOAPUI 5.4



## Rodando a imagem docker


##Testes

Alem dos testes unitarios, a pasta exemplos possui os arquivos SOAPUI para testes das diversas APIs, configuracoes para Swagger
e uma aquivo Jmeter para realizar o teste de todas as operacoes disponiveis


##API - Entrada de dados

A API suporta dois tipos de content-type:
* application/json
* application/xml

O Endpoint é 

http://<server>:<porta: default 9005>/api/censo/{estado}

Onde {estado} é a UF 

###Exemplos

...
http://localhost:9005/api/censo/AC

Request:



...



##Cadastro de templates e estados

 Sao disponibilizadas duas APIs, uma para cadastro de templates e outra para cadastro de Estados.
 Os templates podem ser reutilizados para quantos estados forem precisos, para nao ser necessaria a criacao de templates repetidos caso haja estados que utizem o mesmo template
 Na api / painel de cadastro de estados é possivel selecionar o codigo da template a ser utilizada para o estado escolhido.

##Utilizando interface Web

Basta entrar na interface web (http://<server>:<porta>/)

* Para Estados: http://<server>:<porta>/states
* Para Templates: http://<server>:<porta>/templates

##Utilizando servicos rest

###Modelo Json da template para interface REST

...
      {
      "id": 1,
      "code": "0001",
      "content": "---\r\ninput: json\r\nlistaCidades: cidades\r\ncidade: cidade\r\nnomeCidade: nome\r\nhabitantesCidade: populacao\r\nlistaBairros: bairros\r\nbairro: \r\nnomeBairro: nome\r\nregiaoBairro: \r\nhabitantesBairro: populacao",
      "type": 0
   }
...
Campos:
 *id  : Auto-gerado pelo sistema
 *code: Codigo amigavel, usado pelo objeto Estado para associa-lo a um template (Os templates podem ser reutilizados em multiplos estados) 
 *content: Conteudo da template de transformacao de mensagens
 O campo content (conteudo) está melhor descrito abaixo. 
 
###Modelo de Json de estado para interface REST

{
   "id": 1,
   "stateCode": "AC",
   "templateCode": "0001"
}
 *id  : Auto-gerado pelo sistema
 *stateCode: UF
 *templateCode: Codigo amigavel, usado pelo objeto Estado para associa-lo a um template (Os templates podem ser reutilizados em multiplos estados)  
  
##Templates


 Nota: Quando o arquivo de input nao possuir valor de identificador de cidade ou listaCidades , deixar campo em branco para ignorar

 Campos do conteudo da template:::
 input: Tipo de input se XML ou JSON
 listaCidades: identificador do campo que define uma colecao de cidades 
 cidade: Nome do campo do identificador de cidade
 nomeCidade: Nome do campo do nome da cidade
 habitantesCidade: Nome do campo de habitantes da cidade
 listaBairros: Nome do identificador que marca a coleção de bairros de cada cidade
 bairro: Nome do campo do identificador de bairro
 nomeBairro: Nome do campo do nome de um bairro
 habitantesBairro: Nome do campo de habitantes de um bairro
 regiaoBairro: Nome do campo de regiao de um bairro


 ###Template de exemplo (XML) . Para o arquivo de entrada:
...
 <corpo> 
    <cidade> 
        <nome> Rio de Janeiro</nome>
        <populacao>10345678</populacao>
        <bairros>
            <bairro> 
                <nome> Tijuca</nome>
                <regiao>Zona Norte</regiao>
                <populacao >135678</populacao>
            <bairro>
                <nome> Botafogo</nome>
                <regiao>Zona Sul</regiao>
                <populacao>105711</populacao>
            </bairro>
        </bairros> 
    </cidade> 
 </corpo> 

 O template a ser usado deverá ser:
 input: xml
 listaCidades: corpo
 cidade: cidade
 nomeCidade: nome
 habitantesCidade: populacao
 listaBairros: bairros
 bairro: bairro
 nomeBairro: nome
 regiaoBairro: regiao
 habitantesBairro: populacao
...

### Template de exemplo (JSON)
...

{
    "cities ":[ 
    {
        "name":"Rio Branco", 
        "population":576589,
        "neighborhoods":[
                {
                    "name": "Habitasa",
                    "population":7503
                }
            ]
        }
    ]
 }
 O template a ser usado deverá ser:
 input: json
 listaCidades: cities
 cidade: 
 nomeCidade: name
 habitantesCidade: population
 listaBairros: neighborhoods
 bairro: 
 nomeBairro: name
 regiaoBairro: 
 habitantesBairro: population


...
	 
##Modelo do Template:
---
input: <xml / json>
listaCidades: <cidades>
cidade: <cidade>
nomeCidade: <nome>
habitantesCidade: <habitantes>
listaBairros: <bairros>
bairro: <bairro>
nomeBairro: <nome>
regiaoBairro: <regiao>
habitantesBairro: <habitantes>
    
...