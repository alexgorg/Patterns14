import QtQuick 2.0

Rectangle {
    id: particleList

    width: Math.max(bottomRow.width + 24,topText.width + 28 + buttonLowHigh.width)
    property int indexActiveParticle: listView.currentIndex
    property int widthNumber: 50
    property int widthVelocity: 100
    property int widthAngle: 50
    property int widthAll:208
    property real textAngleOpacity: 1
    property bool sortParticleList: false

    signal buttonRemoveClicked
    signal buttonAddClicked
    signal buttonGraphClicked

    function enableAddButton(){
        buttonAdd.enabled = true;
    }

    function disableAddButton(){
        buttonAdd.enabled = false;
    }
    function enableGraphButton(){
        buttonGraph.enabled = true;
    }

    function disableGraphButton(){
        buttonGraph.enabled = false;
    }
    function enableDeleteButton(){
        buttonDelete.enabled = true;
    }

    function disableDeleteButton(){
        buttonDelete.enabled = false;
    }

    function setIndexActiveParticle(index){
        if(index >= listView.count)
            listView.currentIndex = listView.count-1;
        else if(index < 0)
            listView.currentIndex = 0;
        else
            listView.currentIndex = index;
    }

    Behavior on widthNumber {
        PropertyAnimation{
            duration: 200
            easing.type: Easing.InOutQuad
        }
    }
    Behavior on widthVelocity {
        PropertyAnimation{
            duration: 200
            easing.type: Easing.InOutQuad
        }
    }
    Behavior on widthAngle {
        PropertyAnimation{
            duration: 200
            easing.type: Easing.InOutQuad
        }
    }

    Behavior on widthAll {
        PropertyAnimation{
            duration: 200
            easing.type: Easing.InOutQuad
        }
    }

    Behavior on textAngleOpacity {
        PropertyAnimation{
            duration: 200
            easing.type: Easing.InOutQuad
        }
    }
    Behavior on width {
        PropertyAnimation{
            duration: 200
            easing.type: Easing.InOutQuad
        }
    }

    function updateParticleList(particleInform){
        if(particleInform === undefined)
            return;

        var i;
        listModel.clear();

        //sort
        if(particleList.sortParticleList)
            particleInform.sort(function(a,b){return b.velocity - a.velocity;});
        //----

        for(i = 0; i < particleInform.length; i++){
            listModel.append({"velocity":particleInform[i].velocity,
                                 "angle":particleInform[i].angle});
        }
    }

    Rectangle{
        id:top
        anchors.top: parent.top
        anchors.left: parent.left
        anchors.right: parent.right

        anchors.leftMargin: 4
        anchors.rightMargin: 4
        anchors.topMargin: 4
        anchors.bottomMargin:4

        height: 40

        border.width: 4
        border.color: "darkgray"

        radius: 4

        Text{
            id: topText
            anchors.left: parent.left
            anchors.leftMargin: 8
            anchors.verticalCenter: parent.verticalCenter
            text: "Найденные частицы"
        }

        ButtonRotate{
            id: buttonLowHigh
            anchors.right: parent.right
            anchors.rightMargin: 8
            anchors.verticalCenter: parent.verticalCenter
            source: "img/left.png"

            onClicked: {
                if(particleList.state == "highInform")
                    particleList.state = "lowInform";
                else
                    particleList.state = "highInform";
            }
        }
    }

    Rectangle{
        id: list
        anchors.top: top.bottom
        anchors.bottom: bottom.top
        anchors.left: parent.left
        anchors.right: parent.right

        anchors.leftMargin: 4
        anchors.rightMargin: 4
        anchors.topMargin: 4
        anchors.bottomMargin:4

        border.width: 4
        border.color: "darkgray"
        radius: 4

        Rectangle{
            id: headerList
            width:particleList.widthAll
            height: 25
            anchors.top: parent.top
            anchors.topMargin: 4
            anchors.horizontalCenter: parent.horizontalCenter;
            Row{
                spacing: 4
                Rectangle{
                    id: headerListNumber
                    width:particleList.widthNumber
                    height: headerNumberText.height + 10
                    Text{
                        id: headerNumberText
                        anchors.centerIn: parent
                        text:"<b>№</b>"
                    }
                }
                Rectangle{
                    id: headerListVelocity
                    width:particleList.widthVelocity
                    height: headerVelocityText.height + 10
                    Text{
                        id: headerVelocityText
                        anchors.centerIn: parent
                        text:"<b>Скорость,</b> <i>м/с</i>"
                    }
                }
                Rectangle{
                    id: headerListAngle
                    width:particleList.widthAngle
                    height: headerAngleText.height + 10

                    Behavior on opacity {
                        PropertyAnimation {duration: 200; easing.type: Easing.InOutQuad;}
                    }

                    Text{
                        id: headerAngleText
                        anchors.centerIn: parent
                        text:"<b>Угол,</b> <i>°</i>"
                    }
                }
            }
        }

        ListView {
            id: listView

            anchors.top: headerList.bottom
            anchors.left: parent.left
            anchors.right: parent.right
            anchors.bottom: parent.bottom

            anchors.leftMargin: headerList.x
            anchors.topMargin: 8
            anchors.bottomMargin: 8

            spacing: 4

            model: listModel
            delegate: listDelegate
        }

        Timer {
            id: listTimer
            running: true
            repeat: true
            interval: 100
            property real oldy: 0
            property int cntStop: 0
            onTriggered: {
                var y,v,y1,y2,Y;
                y = listView.contentY;
                v = Math.abs(oldy - y) / 100;
                oldy = y;
                if(scroller.enabled === false){
                    if(v > 2.0){// 2.3 - it's magic :-D
//                        console.log("OK")
                        scroller.enabled = true;
                        scroller.opacity = 0.5;
                        y1 = (list.height - scroller.height - 4 - 25);
                        y2 = listView.contentHeight;
                        Y = listView.contentY;
                        scroller.y = 25+Y * y1 / (y2 - y1);
                    }
                } else {
                    if(scroller.pressed === false) {
                        y1 = (list.height - scroller.height - 4 - 25);
                        y2 = listView.contentHeight;
                        Y = listView.contentY;
                        scroller.y = 25+Y * y1 / (y2 - y1);
                        if(v < 2 && scroller.entered === false)
                            cntStop++;
                        else
                            cntStop = 0;
                        if(cntStop == 15){
                            scroller.enabled = false;
                            scroller.opacity = 0;
                        }
                    } else {
                        cntStop = 0;
                    }
                }
            }
        }

        Rectangle {
            id: scroller
            opacity: enabled ? 0.5:0
            enabled: false
            x: parent.width - width - 4
            y: 25
            color: "orange"
            height: 50
            width: 20
            border.width: 2
            border.color: "black"
            radius: 4

            property bool pressed: scrollerMouse.pressed;
            property bool entered: scrollerMouse.entered;

            Behavior on width {PropertyAnimation{duration:200; easing.type: Easing.InOutQuad;}}
            Behavior on opacity {PropertyAnimation{duration:200; easing.type: Easing.InOutQuad;}}
            Behavior on y {PropertyAnimation{duration:200; easing.type: Easing.InOutQuad;}}

            onYChanged: {
                if(y < 25)
                    y = 25;
                if(y > list.height - scroller.height - 4)
                    y = list.height - scroller.height - 4;
            }

            MouseArea{
                id: scrollerMouse
                anchors.fill: parent
                enabled: parent.enabled
                hoverEnabled: enabled
                drag.target: scroller
                drag.minimumY: 25
                drag.maximumY: list.height - height - 4
                drag.minimumX: list.width - width - 4
                drag.maximumX: list.width - width - 4

                property bool entered: false

                onMouseYChanged: {
                    if(pressed) {
                        var y1 = (list.height - scroller.height - 4 - 25);
                        var y2 = listView.contentItem.height;
                        var y = (scroller.y - 25);
                        listView.contentY = (y2 - y1) * y / y1;
                    }
                }

                onEntered: {
                    parent.opacity = 0.8
                    entered = true;
                }

                onExited: {
                    parent.opacity = 0.5
                    entered = false;
                }
            }
        }


        ListModel{
            id: listModel
        }

        Component{
            id: listDelegate
            Rectangle {
                id:listElementRectangle
                width: widthAll;
                height: numberRectangle.height
                color: listView.currentIndex == index?"orange":"white"
                border.width: listView.currentIndex == index? 2:1
                border.color: "black"
                radius: 4

                opacity:y - listView.contentY + listView.y < 25 ||
                        y - listView.contentY > listView.height - height? 0 : 1

                Behavior on border.width { PropertyAnimation{duration: 200; easing.type: Easing.InOutQuad;} }
                Behavior on color { PropertyAnimation{duration: 200; easing.type: Easing.InOutQuad;} }

                Row {
                    spacing: 4
                    Item {
                        id: numberRectangle
                        width: particleList.widthNumber
                        height: textNumber.height + 10
                        Text{
                            id: textNumber
                            anchors.centerIn: parent
                            text: "<b>" + (index+1) + "</b>"
                        }
                    }
                    Item {
                        id: velocityRectangle
                        width: particleList.widthVelocity
                        height: textVelocity.height + 10
                        Text{
                            id: textVelocity
                            anchors.centerIn: parent
                            text: Math.round(velocity * 100)/100
                        }
                    }
                    Item{
                        id: angleRectangle
                        width: particleList.widthAngle
                        height: textAngle.height + 10
                        Text{
                            id: textAngle
                            opacity: particleList.textAngleOpacity
                            anchors.centerIn: parent
                            text: Math.round((180*angle/Math.PI)*100)/100
                        }
                    }
                }

                MouseArea{
                    anchors.fill: parent
                    onClicked:{
                        listView.currentIndex = index
                    }
                }
            }
        }
    }

    Rectangle {
        id: bottom
        anchors.bottom: parent.bottom
        anchors.left: parent.left
        anchors.right: parent.right

        anchors.margins:  4
        height: bottomRow.height + 16

        border.width: 4
        border.color: "darkgray"

        radius:4

        Rectangle {
            id: bottomRow
            width: buttonDelete.width + 4 + buttonAdd.width
            height: buttonDelete.height + 4 + buttonGraph.height
            x:4
            y:4

            Behavior on height {
                PropertyAnimation{
                    duration: 200
                    easing.type: Easing.InOutQuad
                }
            }
            ButtonImage{
                id: buttonDelete
                x: 4
                y: 4
                enabled: listModel.count !== 0 ? true: false
                source: "img/delete.png"
                text: "Это не частица"
                Behavior on x {
                    PropertyAnimation{
                        duration: 200
                        easing.type: Easing.InOutQuad
                    }
                }
                Behavior on y {
                    PropertyAnimation{
                        duration: 200
                        easing.type: Easing.InOutQuad
                    }
                }

                onClicked: {
                    particleList.buttonRemoveClicked();
                }
            }

            ButtonImage{
                id: buttonAdd
                y: buttonDelete.y
                x: buttonDelete.x + buttonDelete.width + 4
                enabled: false
                source: "img/plus.png"
                text: "Добавить частицу"

                Behavior on x {
                    PropertyAnimation{
                        duration: 200
                        easing.type: Easing.InOutQuad
                    }
                }
                Behavior on y {
                    PropertyAnimation{
                        duration: 200
                        easing.type: Easing.InOutQuad
                    }
                }

                onClicked: {
                    particleList.disableAddButton();
                    particleList.buttonAddClicked();
                }
            }
            ButtonImage{
                id: buttonGraph
                x: buttonDelete.x
                y: buttonDelete.height + buttonDelete.y + 4
                enabled: false
                source: "img/graph.png"
                text: "Построить график"

                Behavior on x {
                    PropertyAnimation{
                        duration: 200
                        easing.type: Easing.InOutQuad
                    }
                }
                Behavior on y {
                    PropertyAnimation{
                        duration: 200
                        easing.type: Easing.InOutQuad
                    }
                }

                onClicked: {
                    particleList.buttonGraphClicked();
                }
            }
        }
    }

    state: "highInform"

    states: [
        State {
            name: "lowInform"
            PropertyChanges {
                target: top

            }
            PropertyChanges{
                target: list

            }
            PropertyChanges {
                target: bottom

            }
            PropertyChanges {
                target: bottomRow
                height: buttonDelete.height
            }
            PropertyChanges {
                target: buttonDelete
                text: ""
            }
            PropertyChanges {
                target: buttonGraph
                text: ""
                x:buttonAdd.x + buttonAdd.width + 4
                y:buttonAdd.y
            }
            PropertyChanges {
                target: buttonAdd
                text: ""
            }
            PropertyChanges {
                target: headerListAngle
                opacity: 0
            }
            PropertyChanges{
                target: particleList
                widthNumber:30
                widthVelocity:80
                widthAngle:0
                widthAll:114
                textAngleOpacity: 0
            }
            PropertyChanges{
                target: scroller
                width:10
            }
        },

        State {
            name: "highInform"
            PropertyChanges {
                target: top

            }
            PropertyChanges{
                target: list

            }
            PropertyChanges {
                target: bottom

            }
            PropertyChanges {
                target: bottomRow
                height: buttonDelete.height + 4 + buttonGraph.height
            }
            PropertyChanges {
                target: buttonDelete
                text: "Это не частица"

            }
            PropertyChanges {
                target: buttonGraph
                text: "Построить график"
                x: buttonDelete.x
                y: buttonDelete.y + buttonDelete.height + 4
            }
            PropertyChanges {
                target: buttonAdd
                text: "Добавить частицу"
            }
            PropertyChanges {
                target: headerListAngle
                opacity: 1
            }

            PropertyChanges{
                target: particleList
                widthNumber:50
                widthVelocity:100
                widthAngle:50
                widthAll: 208
                textAngleOpacity:1
            }
            PropertyChanges{
                target: scroller
                width:20
            }
        }
    ]
}
