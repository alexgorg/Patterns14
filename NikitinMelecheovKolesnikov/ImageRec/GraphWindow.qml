import QtQuick 2.0
import QtQuick.Window 2.0
import QtQuick.Dialogs 1.1

Window {
    id: graphWindow
    width: 800
    height: 400
    visible: false

    property bool cancelButtonClicked: false
    property bool saveButtonClicked: false
    property var particleData

    function cancelClicked(){
        graphWindow.close();

        cancelButtonClicked = true;
    }
    function saveClicked(){
        fileSaveDialog.open();
        saveButtonClicked = true;
    }

    FileDialog {
        id: fileSaveDialog
        title: "Выберете файл для сохранения результатов"
        selectExisting: false
        selectFolder: false
        selectMultiple: false
        nameFilters:  [ "Файл изображения (*.png)"]
        onAccepted: {
            canvas.save(fileUrls[0].substring(8));
        }
    }

    onVisibleChanged: {
        if(!visible) {
            if(!cancelButtonClicked && !saveButtonClicked) {
                cancelClicked();
            }
        } else {
            cancelButtonClicked = false;
            saveButtonClicked = false;
        }
        requestPaint();
    }
    function requestPaint(){
        canvas.requestPaint();
    }

    Row {
        anchors.centerIn: parent
        anchors.margins: 4
        spacing: 4
        Rectangle {
            id: grpahRectangle
            width: 2.0 * graphWindow.width / 3 - 6
            height: graphWindow.height - 4
            border.color: "darkgray"
            border.width: 4
            radius: 4

            Canvas {
                id: canvas
                anchors.fill: parent
                anchors.margins: 3
                antialiasing: true

                property var _x:[]
                property var _y:[]
                property real maxY

                onPaint: myPaint();

                function myPaint(){
                    var ctx = canvas.getContext("2d");

                    ctx.fillStyle = "gray";
                    ctx.fillRect(0,0,canvas.width, canvas.height);

                    prePaintCalc();
                    drawGraph(ctx);

                    velocityText.text = Math.floor(maxY * 100) / 100;
                    cntParticleText.text = graphWindow.particleData.particleInfo.length;
                }

                function prePaintCalc(){
                    var _particleInform = [];
                    var i;
                    for(i = 0; i < graphWindow.particleData.particleInfo.length; i++){
                        _particleInform.push(
                        {
                            i: graphWindow.particleData.particleInfo[i].i,
                            j: graphWindow.particleData.particleInfo[i].j,
                            velocity: graphWindow.particleData.particleInfo[i].velocity,
                            angle: graphWindow.particleData.particleInfo[i].angle
                        });
                    }
                    _particleInform.sort(function (a,b){
                        if(a.j !== b.j)
                            return a.j - b.j;
                        return a.i - b.i;
                    });
                    var imageWidth = graphWindow.particleData.width;
                    var imageHeight = graphWindow.particleData.height;

                    var step = 30;
                    var k;
                    _y = [];
                    _x = [];

                    var cnt;
                    for(k = 0, i = 0; k < imageWidth; k += step) {
                        _y.push(0);
                        _x.push(k);
//                        console.log(_particleInform[i].j + " " + (k + step) + " " + i + " " + _particleInform.length);
                        cnt = 0;
                        while(i < _particleInform.length &&
                              _particleInform[i].j <= k + step){
                            _y[_y.length-1] += _particleInform[i].velocity;
                            i++;
                            cnt++;
                        }
                        if(cnt !== 0)
                            _y[_y.length-1] /= cnt;
                    }
                }

                function drawGraph(ctx){
                    var w = canvas.width,
                            h = canvas.height;
                    ctx.save();
                    var imageWidth = graphWindow.particleData.width;
                    var imageHeight = graphWindow.particleData.height;
                    var canvasWidth = canvas.width;
                    var canvasHeight = canvas.height;
                    var leftMargin = 10;
                    var bottomMargin = 10;

                    var i;
                    var scaleX = imageWidth / (canvasWidth - leftMargin);
                    for(i = 0; i < _x.length; i++){
                        drawLine(ctx,0,(_x[i] ) / scaleX + leftMargin,
                                 canvasHeight,(_x[i] ) / scaleX + leftMargin,"lightgray",1);
                    }
                    maxY = 0;
                    for(i = 0; i < _y.length; i++)
                        maxY = Math.max(maxY, _y[i]);
                    var scaleY = maxY / (canvasHeight - bottomMargin);

//                    console.log(maxY);
                    var xx,yy;
                    for(i = 0; i < _y.length; i++){
                        xx = _x[i] / scaleX + leftMargin;
                        yy = canvasHeight - (_y[i] / scaleY + bottomMargin);
                        drawLine(ctx,yy,0,yy,canvasWidth,"lightgray",1);
                        drawCircle(ctx,yy,xx,"orange",5);
                    }

                    drawLine(ctx,canvasHeight - bottomMargin,0,canvasHeight - bottomMargin,canvasWidth,"black",4);
                    drawLine(ctx,canvasHeight,leftMargin,0,leftMargin,"black",4);

                    ctx.restore();
                }
                function drawLine(ctx,i1,j1,i2,j2,color,width){
                    ctx.beginPath();
                    ctx.strokeStyle = color;
                    ctx.lineWidth = width;
                    ctx.moveTo(j1,i1);
                    ctx.lineTo(j2,i2);
                    ctx.stroke();
                    ctx.closePath();
                }
                function drawCircle(ctx, i, j,color, radius){
                    ctx.fillStyle = color;
                    ctx.strokeStyle = color;
                    ctx.lineWidth = 4;

                    ctx.beginPath();

                    ctx.ellipse(j - radius/2,i - radius/2, radius, radius);
                    ctx.stroke();
                    ctx.fill();

                    ctx.closePath();
                }
            }
        }
        Rectangle {
            id: buttonsRectangle
            width: graphWindow.width / 3 - 6
            height: graphWindow.height - 4
            border.color: "darkgray"
            border.width: 4
            radius: 4

            Row {
                spacing: 4
                anchors.left: parent.left
                anchors.top: parent.top
                anchors.leftMargin: 8
                anchors.topMargin: 8
                Column {
                    spacing: (velocityCountColumn.height - textMaximalVelocity.height - textCountPaticle.height) / 2
                    y: 5

                    Text {
                        id: textMaximalVelocity
                        text: "Максимальная средняя скорость"
                    }
                    Text {
                        id: textCountPaticle
                        text: "Количество частиц"
                    }
                }

                Column {
                    id: velocityCountColumn
                    spacing: 4
                    Rectangle {
                        id: velocityTextRectangle
                        height: velocityText.height + 10
                        width: 50
                        border.color: "darkgray"
                        border.width: 2
                        radius: 4

                        Text {
                            id: velocityText
                            anchors.centerIn: parent
                            text: " "
                        }
                    }
                    Rectangle {
                        id: cntParticleTextRectangle
                        height: cntParticleText.height + 10
                        width: 50
                        border.color: "darkgray"
                        border.width: 2
                        radius: 4

                        Text {
                            id: cntParticleText
                            anchors.centerIn: parent
                            text: " "
                        }
                    }
                }
            }

            ButtonText{
                id: cancelButton
                anchors.right: saveButtom.left
                anchors.bottom: parent.bottom
                anchors.rightMargin: 4
                anchors.bottomMargin: 8
                text: "Отмена"
                onClicked: {
                    graphWindow.cancelClicked();
                }
            }

            ButtonText {
                id: saveButtom
                anchors.right: parent.right
                anchors.bottom: parent.bottom
                anchors.rightMargin: 8
                anchors.bottomMargin: 8
                text: "Сохранить *.png"
                onClicked: {
                    graphWindow.saveClicked();
                }
            }
        }
    }

    Item {
        anchors.fill: parent
        focus: true
        Keys.onEscapePressed: {
            graphWindow.cancelClicked();
        }
    }
}
