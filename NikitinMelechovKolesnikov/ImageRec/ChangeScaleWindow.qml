import QtQuick 2.0
import QtQuick.Window 2.0
import QtQuick.Dialogs 1.1

Window {
    id: changeScaleWindow
    modality: Qt.WindowModal
    title: "Изменить масштаб"

    signal accepted

    property string imageName: "undefined"
    property string oldImageName: "undefined"
    property real currentScale: 1

    property string bufImageName: "undefined"
    property string bufOldImageName: "undefined"
    property real   bufCurrentScale: 1
    property var    bufImage: undefined
    property int    bufI1: 0
    property int    bufJ1: 0
    property int    bufI2: 0
    property int    bufJ2: 0
    property real   bufOriginX: 0
    property real   bufOriginY: 0
    property real   bufScaleX:  1
    property real   bufScaleY:  1
    property int    bufCntPointDraw: 0

    property bool cancelButtonClicked: false
    property bool okButtonClicked: false

    function popInformation() {
        changeScaleWindow.currentScale = changeScaleWindow.bufCurrentScale;

        canvas.image = changeScaleWindow.bufImage;
        canvas.cntPointDraw = changeScaleWindow.bufCntPointDraw;

        canvas.i1 = changeScaleWindow.bufI1;
        canvas.j1 = changeScaleWindow.bufJ1;
        canvas.i2 = changeScaleWindow.bufI2;
        canvas.j2 = changeScaleWindow.bufJ2;

        canvas.originX = changeScaleWindow.bufOriginX;
        canvas.originY = changeScaleWindow.bufOriginY;
        canvas.scaleX  = changeScaleWindow.bufScaleX;
        canvas.scaleY  = changeScaleWindow.bufScaleY;

        changeScaleWindow.imageName = changeScaleWindow.bufImageName;
        changeScaleWindow.oldImageName = changeScaleWindow.bufOldImageName;
    }
    function pushInformation() {
        changeScaleWindow.bufCurrentScale = changeScaleWindow.currentScale;
        changeScaleWindow.bufImage = canvas.image;
        changeScaleWindow.bufCntPointDraw = canvas.cntPointDraw;

        changeScaleWindow.bufI1 = canvas.i1;
        changeScaleWindow.bufJ1 = canvas.j1;
        changeScaleWindow.bufI2 = canvas.i2;
        changeScaleWindow.bufJ2 = canvas.j2;

        changeScaleWindow.bufOriginX = canvas.originX;
        changeScaleWindow.bufOriginY = canvas.originY;
        changeScaleWindow.bufScaleX = canvas.scaleX;
        changeScaleWindow.bufScaleY = canvas.scaleY;

        changeScaleWindow.bufImageName = changeScaleWindow.imageName;
        changeScaleWindow.bufOldImageName = changeScaleWindow.oldImageName;
    }
    function cancelClicked(){
        changeScaleWindow.popInformation();

        canvas.requestPaint();
        changeScaleWindow.close();

        cancelButtonClicked = true;
    }
    function okClicked(){
        if(canvas.cntPointDraw !== 2) {
            canvas.cntPointDraw = 0;
        }
        pushInformation();

        canvas.requestPaint();
        changeScaleWindow.close();

        changeScaleWindow.accepted();

        okButtonClicked = true;
    }

    onImageNameChanged: {
        canvas.reloadImage(oldImageName, imageName);
        oldImageName = imageName;
    }

    onVisibleChanged: {
        if(!visible) {
            if(!cancelButtonClicked && !okButtonClicked){
                cancelClicked();
            }
        } else {
            cancelButtonClicked = false;
            okButtonClicked = false;
        }
    }

    FileDialog {
        id: imageLoadDialog
        title: "Выберете файл изображения линейки"
        nameFilters:  [ "Файлы изображений (*.png *.bmp)"]
        onAccepted: {
            changeScaleWindow.imageName = fileUrl;
        }
    }

    Row {
        anchors.centerIn: parent
        anchors.margins: 4
        spacing: 4
        Rectangle {
            id: buttonsRectangle
            width: changeScaleWindow.width / 2 - 6
            height: changeScaleWindow.height - 4
            border.color: "darkgray"
            border.width: 4
            radius: 4
            Row {
                spacing: 4
                anchors.margins: 8
                anchors.left: parent.left
                anchors.top: parent.top
                Column {
                    y: ( - textImageLine.height + textScaleRectangle.height ) / 2
                    spacing: buttonsColumn.height - textImageLine.height -
                             textZoom.height -
                             ( - textImageLine.height + textScaleRectangle.height )
                    Text {
                        id: textImageLine
                        text: "Изображение линейки"
                    }
                    Text {
                        id: textZoom
                        text: "Масштаб"
                    }
                }

                Column {
                    id: buttonsColumn
                    spacing: 4

                    ButtonText {
                        id: buttonLoad
                        text: "Загрузить"
                        onClicked: {
                            imageLoadDialog.open();
                        }
                    }

                    Rectangle {
                        id: textScaleRectangle
                        radius: 4
                        border.color: "darkgray"
                        border.width: 2
                        width: buttonLoad.width
                        height: textScale.height + 10
                        Text{
                           id: textScale
                           anchors.centerIn: parent
//                           cursorVisible: true
                           text: "1 : " +  Math.floor(changeScaleWindow.currentScale)
                        }
                    }
                }
            }

            ButtonText {
                id: cancelButton
                anchors.leftMargin: 4
                anchors.bottomMargin: 8
                anchors.rightMargin: 4
                anchors.topMargin: 4
                text: "Отмена"
                anchors.bottom: parent.bottom
                anchors.right: applyButton.left
                onClicked:  {
                    changeScaleWindow.cancelClicked();
                }
            }

            ButtonText {
                id: applyButton
                anchors.leftMargin: 4
                anchors.bottomMargin: 8
                anchors.rightMargin: 8
                anchors.topMargin: 4
                text: "   Oк   "
                anchors.right: parent.right
                anchors.bottom: parent.bottom
                onClicked: {
                    changeScaleWindow.okClicked();
                }
            }
        }
        Rectangle {
            id: imageRectangle
            width: changeScaleWindow.width / 2 - 6
            height: changeScaleWindow.height - 4
            border.color: "darkgray"
            border.width: 4
            radius: 4

            Canvas{
                id: canvas
                anchors.fill: parent
                anchors.margins: 3
                antialiasing: true

                property var image
                property real originX:0
                property real originY:0
                property real scaleX: 1
                property real scaleY: 1
                property var particleInform:[]
                property real i1:0
                property real j1:0
                property real i2:0
                property real j2:0
                property int cntPointDraw: 0
                property real sizePoint: 10

                Behavior on scaleX {
                    NumberAnimation{
                        duration: 200
                    }
                }
                Behavior on scaleY {
                    NumberAnimation{
                        duration: 200
                    }
                }
                Behavior on originX {
                    NumberAnimation{
                        id: animationOriginX
                        duration: 150
                    }
                }
                Behavior on originY {
                    NumberAnimation{
                        id: animationOriginY
                        duration: 150
                    }
                }

                onImageLoaded: {
                    ctxloadImage(canvas.getContext("2d"));
                    cntPointDraw = 0;
                    i1 = j1 = i2 = j2 = 0;
                    requestPaint();
                }
                onScaleXChanged: requestPaint();
                onScaleYChanged: requestPaint();
                onOriginXChanged: requestPaint();
                onOriginYChanged: requestPaint();
                onWidthChanged: requestPaint();
                onHeightChanged: requestPaint();

                onPaint: mypaint();

                function mypaint(){
                    var ctx = canvas.getContext("2d");
                    ctx.save();

                    ctx.fillStyle = "gray";
                    ctx.fillRect(0,0,canvas.width, canvas.height);
                    ctx.scale(scaleX, scaleY);
                    ctx.translate(originX, originY);

                    if(image !== undefined)
                        ctx.putImageData(image,0,0);

                    if(cntPointDraw == 1) {
                        var _i2 = canvasMouse.mouseY / scaleX  - originY,
                                _j2 = canvasMouse.mouseX / scaleX - originX;

                        ctx.strokeStyle = "orange"
                        ctx.lineWidth = 2;

                        ctx.moveTo(_j2 + sizePoint/2,_i2 + sizePoint/2);

                        ctx.lineTo(j1 + sizePoint/2,i1 + sizePoint/2);
                        ctx.stroke();

                        drawCircle(ctx, j1, i1,sizePoint);
                        drawCircle(ctx, _j2, _i2,sizePoint);
                    } else if(cntPointDraw == 2) {
                        ctx.strokeStyle = "orange";
                        ctx.lineWidth = 2;

                        ctx.moveTo(j2 + sizePoint/2,i2 + sizePoint/2);

                        ctx.lineTo(j1 + sizePoint/2,i1 + sizePoint/2);
                        ctx.stroke();

                        drawCircle(ctx, j1, i1,sizePoint);
                        drawCircle(ctx, j2, i2,sizePoint);
                    }
                    ctx.restore();
                }

                function drawCircle(ctx, i, j, radius){
                    ctx.save();

                    ctx.fillStyle = "white";
                    ctx.strokeStyle = "orange";
                    ctx.lineWidth = 4;

                    ctx.beginPath();

                    ctx.ellipse(i,j, radius, radius);
                    ctx.stroke();
                    ctx.fill();

                    ctx.closePath();

                    ctx.restore();
                }

                function ctxloadImage(ctx){
                    image = ctx.createImageData(changeScaleWindow.imageName);
                }

                function zoomIn(mouseX, mouseY){
                    scaleX += 0.1;
                    scaleY += 0.1;
                    var goalX = canvas.width/2;
                    var goalY = canvas.height/2;
                    var dvx = 0.5 * (goalX - mouseX) / (scaleX + 0.1);
                    var dvy = 0.5 * (goalY - mouseY) / (scaleY + 0.1);
                    translateOriginX(originX + dvx, scaleX + 0.1);
                    translateOriginY(originY + dvy, scaleY + 0.1);
                }

                function zoomOut(mouseX, mouseY){
                    if(scaleX < 0.2){
                        scaleX = 0.2;
                        translateOriginX(originX,0.2);
                    } else {
                        scaleX -= 0.1
                        translateOriginX(originX,scaleX - 0.1);
                    }

                    if(scaleY < 0.2) {
                        scaleY = 0.2;
                        translateOriginY(originY,0.2);
                    } else {
                        scaleY -= 0.1
                        translateOriginY(originY,scaleY - 0.1);
                    }
                }

                function translateOriginX(newOriginX,scaleX){
                    var realImageWidth = image.width * scaleX;

                    var realDragWidth = (canvas.width - realImageWidth) / scaleX;
                    if(canvas.width >= realImageWidth){
                        if(newOriginX >= 0 && newOriginX < realDragWidth)
                            originX = newOriginX;
                        else if(newOriginX < 0)
                            originX = 0;
                        else if(newOriginX >= realDragWidth)
                            originX = realDragWidth;
                    } else {
                        if(newOriginX > 0)
                            originX = 0;
                        else if(newOriginX < realDragWidth)
                            originX = realDragWidth;
                        else
                            originX = newOriginX;
                    }
                }

                function translateOriginY(newOriginY,scaleY){
                    var realImageHeight = image.height * scaleY;
                    var realDragHeight = (canvas.height - realImageHeight) / scaleY;
                    if(canvas.height > realImageHeight){
                        if(newOriginY >= 0 && newOriginY < realDragHeight)
                            originY = newOriginY;
                        else if(newOriginY < 0)
                            originY = 0;
                        else if(newOriginY >= realDragHeight)
                            originY = realDragHeight;
                    } else {
                        if(newOriginY > 0)
                            originY = 0;
                        else if(newOriginY < realDragHeight)
                            originY = realDragHeight;
                        else
                            originY = newOriginY;
                    }
                }

                function reloadImage(oldImageFile, newImageFile){
                    if(oldImageFile !== "undefined"){
                        unloadImage(oldImageFile);
                    }
                    if(newImageFile !== "undefined") {
                        loadImage(newImageFile);
                    }
                }

                MouseArea{
                    id: canvasMouse

                    enabled: changeScaleWindow.imageName != "undefined";

                    hoverEnabled: enabled
                    anchors.fill: parent

                    cursorShape: Qt.OpenHandCursor
                    acceptedButtons: Qt.AllButtons

                    property real pressedX
                    property real pressedY
                    property real oldOriginX
                    property real oldOriginY
                    onPressed: {
                        if(pressedButtons == Qt.MiddleButton){
                            if(changeScaleWindow.enableScaleChange){

                            } else {
                                pressedX = mouseX
                                pressedY = mouseY
                                oldOriginX = canvas.originX
                                oldOriginY = canvas.originY
                                cursorShape = Qt.ClosedHandCursor
                            }
                        } else if(pressedButtons == Qt.LeftButton){
                            if(canvas.cntPointDraw == 0) {
                                canvas.j1 = mouseX / canvas.scaleX - canvas.originX;
                                canvas.i1 = mouseY / canvas.scaleY - canvas.originY;
                                canvas.cntPointDraw++;
                            } else if(canvas.cntPointDraw == 1){
                                canvas.j2 = mouseX / canvas.scaleX - canvas.originX;
                                canvas.i2 = mouseY / canvas.scaleY - canvas.originY;
                                canvas.cntPointDraw++;

                                //update scale param
                                var w = Math.abs(canvas.j1 - canvas.j2),
                                        h = Math.abs(canvas.i1 - canvas.i2);
                                changeScaleWindow.currentScale = Math.sqrt(w * w + h * h);
                            } else {
                                canvas.cntPointDraw = 0;
                            }
                        }
                    }

                    onReleased: {
                        if(changeScaleWindow.enableScaleChange){

                        } else {
                            cursorShape = Qt.OpenHandCursor
                        }
                    }
                    onMouseXChanged: {
                        if(pressedButtons == Qt.MiddleButton){
                            if(changeScaleWindow.enableScaleChange){

                            } else {
                                animationOriginX.duration = 0;
                                canvas.translateOriginX(oldOriginX - (pressedX - mouseX) / canvas.scaleX,canvas.scaleX);
                            }
                        }
                        canvas.requestPaint();
                    }
                    onMouseYChanged: {
                        if(pressedButtons == Qt.MiddleButton){
                            if(changeScaleWindow.enableScaleChange){

                            } else {
                                animationOriginY.duration = 0;
                                canvas.translateOriginY(oldOriginY - (pressedY - mouseY) / canvas.scaleY, canvas.scaleY);
                            }
                        }
                        canvas.requestPaint();
                    }
                    onWheel: {
                        if(changeScaleWindow.enableScaleChange){

                        } else {
                            animationOriginX.duration = animationOriginY.duration = 200;
                            if (wheel.angleDelta.y > 0){
                                canvas.zoomIn(wheel.x, wheel.y);
                            } else {
                                canvas.zoomOut(wheel.y, wheel.y);
                            }
                        }
                    }
                }
            }
        }
    }

    Item {
        anchors.fill: parent
        focus: true
        Keys.onEscapePressed: {
            changeScaleWindow.cancelClicked();
        }
    }
}
