import QtQuick 2.0
import "ImageRecognition.js" as ImageRecognition

Rectangle {
    id: imageWindow
    border.color: "darkgray"

    border.width: 4
    radius: 4

    anchors.leftMargin: 0
    anchors.rightMargin: 4
    anchors.topMargin: 4
    anchors.bottomMargin: 4

    property string imageName: "undefined"
    property string oldImageName: "undefined"
    property int indexActiveParticle: 0
    property bool enableMagicPlus: false
    property var particleData

    property real tdelay: 6
    property real horizontAngle: 0
    property real magicParam: 50
    property real scaleParam: 1
    property real binarizationParam: 50

    property real i1: 0
    property real j1: 0
    property real i2: 0
    property real j2: 0
    property bool drawHorizontalLine: false

    signal imageLoaded
    signal particleCreated
    signal particleListCreated

    onIndexActiveParticleChanged: {
        canvas.requestPaint();
    }

    onImageNameChanged: {
        canvas.reloadImage(oldImageName, imageName);
        oldImageName = imageName;
    }

    function makeHorizontalLine(angle){
        if(angle >= 0) {
            i1 = canvas.height;
            j1 = 0;
            i2 = canvas.height - canvas.width * Math.tan(angle);
            j2 = canvas.width
            drawHorizontalLine = true;
            requestPaint();
        } else {
            i2 = canvas.height;
            j2 = canvas.width;
            i1 = canvas.height - canvas.width * Math.tan(-angle);
            j1 = 0;
            if(i1 < 0){
                i1 = 0;
                j1 = 0;
                i2 = canvas.height;
                j2 = canvas.height * Math.tan(Math.PI / 2 + angle);
            }

            drawHorizontalLine = true;
            requestPaint();
        }

        horizontalLineTimer.running = true;
        horizontalLineTimer.start();
    }

    function updateCalcParam(tdelay, horizontAngle, magicParam, scaleParam, binarizationParam){
        imageWindow.tdelay = tdelay;
        imageWindow.horizontAngle = horizontAngle;
        imageWindow.magicParam = magicParam * 100;
        imageWindow.scaleParam = scaleParam;
        imageWindow.binarizationParam = binarizationParam * 100;
    }

    function enableMagicPlusButton(){
        enableMagicPlus = true;
        canvasMouse.cursorShape = Qt.CrossCursor;
    }

    function disableMagicPlusButton(){
        enableMagicPlus = false;
        canvasMouse.cursorShape = Qt.OpenHandCursor;
    }

    function requestPaint(){
        canvas.requestPaint();
    }

    function autoSelectPartilce(){
        particleData.particleInfo = ImageRecognition.getSegmentAuto(canvas.image,tdelay,
                                                                    horizontAngle,scaleParam,binarizationParam);
        imageWindow.particleListCreated();
        canvas.requestPaint();
    }

    function createMagicParticle(i,j){
        particleData.bufParticle = ImageRecognition.magic(
                    canvas.image,i,j,tdelay,horizontAngle,magicParam,scaleParam);
        requestPaint();
        imageWindow.particleCreated();
    }

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
            imageWindow.imageLoaded();
            particleData.width = image.width;
            particleData.height = image.height;
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

            if(particleData.bufParticle !== undefined){
                drawParticle(ctx,particleData.bufParticle,-1);
            }

            ctx.restore();

            if(imageWindow.drawHorizontalLine){
                ctx.strokeStyle = "orange";
                ctx.lineWidth = 4;
                ctx.beginPath();
                ctx.moveTo(imageWindow.j1,imageWindow.i1);
                ctx.lineTo(imageWindow.j2,imageWindow.i2);
                ctx.stroke();
                ctx.closePath();
                drawCircle(ctx,i1 - 10,j1-10,20);
            }

            drawParticleInform(ctx);
        }

        function drawCircle(ctx, i, j, radius){
            ctx.save();

            ctx.fillStyle = "white";
            ctx.strokeStyle = "orange";
            ctx.lineWidth = 8;

            ctx.beginPath();

            ctx.ellipse(j,i, radius, radius);
            ctx.stroke();
            ctx.fill();

            ctx.closePath();

            ctx.restore();
        }

        function ctxloadImage(ctx){
            image = ctx.createImageData(imageWindow.imageName);
        }

        function drawParticleInform(ctx){
            if(particleData.particleInfo === undefined)
                return;
            ctx.save();

            ctx.scale(scaleX, scaleY);
            ctx.translate(originX, originY);

            var i;
            for(i = 0; i < particleData.particleInfo.length; i++)
                drawParticle(ctx,particleData.particleInfo[i],i);

            ctx.restore();
        }

        function drawParticle(ctx, particle, indexParticle){
            var _i, _j, _w, _h;
            _i = particle.i;
            _j = particle.j;
            _w = particle.width;
            _h = particle.height;
            if(indexParticle === imageWindow.indexActiveParticle){
                ctx.strokeStyle = "blue";
                ctx.lineWidth = 2;
            } else {
                ctx.strokeStyle = "orange";
                ctx.lineWidth = 1;
            }
            ctx.beginPath();
            ctx.moveTo(_j, _i);
            ctx.lineTo(_j + _w, _i);
            ctx.lineTo(_j + _w, _i + _h);
            ctx.lineTo(_j, _i + _h);
            ctx.lineTo(_j,_i);
            ctx.stroke();
            ctx.closePath();
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

            enabled: imageWindow.imageName != "undefined";
            hoverEnabled: enabled
            anchors.fill: parent
            cursorShape: Qt.OpenHandCursor
            acceptedButtons: Qt.AllButtons

            property real pressedX
            property real pressedY
            property real oldOriginX
            property real oldOriginY
            onPressed: {
                if(imageWindow.enableMagicPlus && pressedButtons == Qt.LeftButton){
                    var i = mouseY / canvas.scaleY - canvas.originY,
                            j = mouseX / canvas.scaleX - canvas.originX;
                    var w = canvas.image.width,
                            h = canvas.image.height;
                    if(i >= 0 && i < h && j >= 0 && j < w){
                        imageWindow.createMagicParticle(i,j);
                    }
                } else if(pressedButtons == Qt.MiddleButton){
                    pressedX = mouseX
                    pressedY = mouseY
                    oldOriginX = canvas.originX
                    oldOriginY = canvas.originY
                    cursorShape = Qt.ClosedHandCursor
                }
            }

            onReleased: {
                if(imageWindow.enableMagicPlus){
                    imageWindow.disableMagicPlusButton();
                } else {
                    cursorShape = Qt.OpenHandCursor
                }
            }

            onMouseXChanged: {
                if(pressed){
                    if(imageWindow.enableMagicPlus){

                    } else if(pressedButtons == Qt.MiddleButton){
                        animationOriginX.duration = 0;
                        canvas.translateOriginX(oldOriginX - (pressedX - mouseX) / canvas.scaleX,canvas.scaleX);
                    }
                }
            }
            onMouseYChanged: {
                if(pressed){
                    if(imageWindow.enableMagicPlus){

                    } else if(pressedButtons == Qt.MiddleButton){
                        animationOriginY.duration = 0;
                        canvas.translateOriginY(oldOriginY - (pressedY - mouseY) / canvas.scaleY, canvas.scaleY);
                    }
                }
            }
            onWheel: {
                if(imageWindow.enableMagicPlus){

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

    Timer {
        id: horizontalLineTimer
        running: false
        repeat: false
        interval: 5 * 1000
        onTriggered: {
            drawHorizontalLine = false;
            requestPaint();
        }
    }
}
