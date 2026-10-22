import xmlrpc.client, datetime

servidor = xmlrpc.client.ServerProxy("http://localhost:1099/")

hoje = servidor.hoje_eh()


# convert the ISO8601 string to a datetime object
data_hora_convertida = datetime.datetime.strptime(hoje.value, "%Y%m%dT%H:%M:%S")
print("Hoje é: %s" % data_hora_convertida.strftime("%d.%m.%Y, %H:%M:%S"))

nome = 'João Pedro Toaldo'
email = servidor.gerar_email(nome)
print(f'Email: {email}')


frase = 'A turma de sistemas distribuídos da UFN é composta de alunos do sexo masculino. ' \
'Os alunos são divididos em grupos: os que moram foram de Santa Maria; os que jogam volei; e os ' \
'que agonizam com java.'

frase_sem_artigos = servidor.retirar_artigos(frase)
print(f'Frase sem artigos: ', frase_sem_artigos)