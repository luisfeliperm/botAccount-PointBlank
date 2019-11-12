# POINT BLANK PRIVATE

- Este bot tem como objetivo inundar a tabela "accounts" do servidor de PointBlank Pirata
- Para que funcione, o servidor deve estar com o "auto_create" habilitado

## FUNCIONAMENTO

- Ao realizar a conexão com o servidor "auth", será recebido o opcode 2049 contendo informações essenciais como "sessionId" e "hash", isto será usado para criptografar os pacotes usando o método de deslocamento de bit
- Em seguida, o bot enviará a quantidade configurada de requisições (BASE_LOGIN_REC) 


![enter image description here](https://i.imgur.com/K1EeaIm.png)
