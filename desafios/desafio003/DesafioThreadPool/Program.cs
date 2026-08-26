using DesafioThreadPool.Controller;
using DesafioThreadPool.View;

var controller = new LogController(3);

var resultado = controller.Processar("../erro.log", 50);

var view = new LogView();

view.Exibir(resultado);