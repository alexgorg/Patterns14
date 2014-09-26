import QtQuick 2.0
import QtQuick.Dialogs 1.1

Rectangle {
    id: menuTab
    anchors.leftMargin: 4
    anchors.rightMargin: 4
    anchors.topMargin: 4
    anchors.bottomMargin:4

    anchors.left: parent.left
    anchors.right: parent.right
    anchors.top: parent.top

    property string imageFile
    property string jobResultsFile

    property real binarizationThreshold:0.5
    property real horizontalAngle: numericHorizontal.value / 180 * Math.PI;
    property real magicParam: sliderMagic.value
    property real tdelay: numericUpDownDelay.value
    property bool sortSelect: checkBoxSort.checked

    signal fileChoosed
    signal jobResultsFileChoosed
    signal buttonAutoSelectClicked
    signal buttonMagicPlusClicked
    signal buttonHelpClicked
    signal buttonInfoClicked
    signal buttonScaleClicked

    function enableAutoSelectButton(){
        autoSelectButton.enabled = true;
//        sliderThreshold.enabled = true;
    }
    function disableAutoSelectButton(){
        autoSelectButton.enabled = false;
//        sliderThreshold.enabled = false;
    }
    function enableSaveButton(){
//        saveButton.enabled = true;
    }
    function disableSaveButton(){
//        saveButton.enabled = false;
    }
    function enableMagicPlusButton(){
        buttonMagicPlus.enabled = true;
        sliderMagic.enabled = true;
    }
    function disableMagicPlusButton(){
        buttonMagicPlus.enabled = false;
        sliderMagic.enabled = false;
    }

    FileDialog{
        id: fileOpenDialog
        title: "Выберете файл изображения"
        nameFilters:  [ "Файлы изображений (*.png *.bmp)"]
        onAccepted: {
            menuTab.imageFile = fileUrl;
            menuTab.fileChoosed();
        }
    }
    FileDialog {
        id: fileSaveDialog
        title: "Выберете файл для сохранения результатов"
        onAccepted: {
            menuTab.jobResultsFile = fileUrl;
            menuTab.jobResultsFileChoosed();
        }
    }

    Behavior on height {
        PropertyAnimation{
            duration: 200
            easing.type: Easing.InOutQuad
        }
    }

    Row{
        id: titleBar
        height: homeBar.height
        y: 6
        spacing: 4

        Rectangle{
            id: homeBar

            border.color: "darkgray"
            border.width: 4
            radius: 4

            width: homeBarTitleRow.width + 20
            height: homeBarTitleRow.height + 10

            Behavior on border.color {
                PropertyAnimation{
                    duration: 200
                    easing.type: Easing.InOutQuad
                }
            }

            Behavior on color {
                PropertyAnimation{
                    duration: 200
                    easing.type: Easing.InOutQuad
                }
            }

            Behavior on scale {
                PropertyAnimation{
                    duration: 200
                    easing.type: Easing.InOutQuad
                }
            }

            Row{
                id: homeBarTitleRow
                spacing: 4
                anchors.centerIn: parent
                Image {
                    id: homeBarImage
                    width: homeTitle.height
                    height: homeTitle.height
                    source: "img/home.png"
                }
                Text{
                    id: homeTitle
                    font.pixelSize: 16
                    text:"Главная"
                }
            }

            MouseArea{
                id: homeMouse
                hoverEnabled: true
                anchors.fill: parent
                onClicked: {
                    menuTab.state = "activeHome"
                }
                onEntered: {
                    parent.border.color = "orange"
                }
                onExited: {
                    parent.border.color = "darkgray"
                }
            }
        }
        Rectangle{
            id: optionBar

            border.color: "darkgray"
            border.width: 4
            radius: 4

            width:optionBarTitleRow.width + 20
            height: optionBarTitleRow.height + 10

            Behavior on border.color {
                PropertyAnimation{
                    duration: 200
                    easing.type: Easing.InOutQuad
                }
            }

            Behavior on color {
                PropertyAnimation{
                    duration: 200
                    easing.type: Easing.InOutQuad
                }
            }

            Behavior on scale {
                PropertyAnimation{
                    duration: 200
                    easing.type: Easing.InOutQuad
                }
            }

            Row{
                id:optionBarTitleRow
                spacing: 4
                anchors.centerIn: parent
                Image {
                    id: optionBarImage
                    width: optionTitle.height
                    height: optionTitle.height
                    source: "img/option.png"
                }
                Text{
                    id: optionTitle
                    font.pixelSize: 16
                    text:"Настройки"
                }
            }

            MouseArea{
                id: optionMouse
                hoverEnabled: true
                anchors.fill: parent
                onClicked: {
                    menuTab.state = "activeOption"
                }
                onEntered: {
                    parent.border.color = "orange"
                }
                onExited: {
                    parent.border.color = "darkgray"
                }
            }
        }
    }
    Rectangle{
        id: container

        border.color: "darkgray"
        border.width: 4
        radius: 4

        x:0
        y:titleBar.height + titleBar.y + 4

        width: parent.width
        height: parent.height - y

        Behavior on height {
            PropertyAnimation{
                duration: 200
                easing.type: Easing.InOutQuad
            }
        }

        Rectangle {
            id: containerHome

            anchors.fill: parent
            anchors.leftMargin: 4
            anchors.rightMargin: 4
            anchors.topMargin: 4
            anchors.bottomMargin:4

            Behavior on opacity {
                PropertyAnimation{
                    duration: 200
                    easing.type: Easing.InOutQuad
                }
            }

            Row{
                id: containerHomeRow
                opacity: parent.opacity
                spacing: 4

                anchors.fill: parent
                anchors.leftMargin: 4
                anchors.rightMargin: 4
                anchors.topMargin: 4
                anchors.bottomMargin:4

                Column{
                    spacing: 4
                    ButtonImage{
                        id: buttonOpen
                        source: "img/open.png"
                        text:"Открыть"
                        opacity: containerHome.opacity
                        onClicked: {
                            fileOpenDialog.open();
                        }
                    }
//                    ButtonImage{
//                        id: buttonSave
//                        source: "img/save.png"
//                        text:"Сохранить"
//                        enabled: false
//                        opacity: containerHome.opacity
//                        onClicked: {
//                            fileSaveDialog.open();
//                        }
//                    }
                }
                Separator{
                    height: parent.height
                }
                Column{
                    spacing: 7
                    ButtonText{
                        id: autoSelectButton
                        text:"Автовыделение"
                        opacity: containerHome.opacity
                        enabled: false
                        onClicked: {
                            menuTab.buttonAutoSelectClicked();
                        }
                    }
//                    SliderText{
//                        id: sliderThreshold
//                        enabled: false
//                        width: autoSelectButton.width + 20
//                        height: 10
//                        text: "Порог бинаризации"
//                        opacity: containerHome.opacity
//                    }
                }
                Separator{
                    height: parent.height
                }
                Column{
                    spacing: 0
                    ButtonImage{
                        id: buttonMagicPlus
                        enabled: false
                        source: "img/magicPlus.png"
                        text: "Волшебная палочка"
                        opacity: containerHome.opacity
                        onClicked: {
                            menuTab.buttonMagicPlusClicked();
                        }
                    }
                    SliderText{
                        id: sliderMagic
                        enabled: false
                        width: buttonMagicPlus.width + 20
                        height: 10
                        text: "Чувствительность"
                        opacity: containerHome.opacity
                    }
                }
                Separator{
                    height: parent.height
                }
                Column{
                    spacing: 4
                    ButtonImage{
                        id: buttonHelp
                        source: "img/help.png"
                        text: "Справка"
                        opacity: containerHome.opacity
                        onClicked: {
                            menuTab.buttonHelpClicked();
                        }
                    }
                    ButtonImage{
                        source: "img/info.png"
                        text: "О программе"
                        opacity: containerHome.opacity
                        onClicked: {
                            menuTab.buttonInfoClicked();
                        }
                    }
                }
            }
        }
        Rectangle{
            id: containerOption

            anchors.fill: parent
            anchors.leftMargin: 4
            anchors.rightMargin: 4
            anchors.topMargin: 4
            anchors.bottomMargin:4

            Behavior on opacity {
                PropertyAnimation{
                    duration: 200
                    easing.type: Easing.InOutQuad
                }
            }

            Row{
                id: containerOptionRow
                opacity: parent.opacity
                spacing: 4

                anchors.fill: parent
                anchors.leftMargin: 4
                anchors.rightMargin: 4
                anchors.topMargin: 4
                anchors.bottomMargin:4

                Column{
                    spacing: 4
                    CheckBoxText{
                        id: checkBoxSort
                        text: "Сортировка по скорости"
                        opacity: containerOption.opacity
                    }
//                    CheckBoxText{
//                        text: "Развернутый список"
//                        opacity: containerOption.opacity
//                    }
                }
                Separator{
                    height: parent.height
                }
                Column{
                    spacing: 4
                    NumericUpDownText{
                        id: numericUpDownDelay
                        width: 150
                        text: "Время задержки"
                        value:6
                        minValue: 1
                        maxValue: 999
                        infoText: "мс"
                        opacity: containerOption.opacity
                    }

                    TextRadioTwoButton {
                        text: "Положение сопла"
                        button1Text: "слева"
                        button2Text: "справа"

                        opacity: containerOption.opacity
                    }
                }
                Column {
                    spacing: 4
                    NumericUpDownText{
                        id: numericHorizontal
                        width: 150
                        text: "Горизонталь"
                        value: 0
                        minValue: -90
                        maxValue: 90
                        infoText: "гр"
                        opacity: containerOption.opacity
                    }
                    ButtonImage{
                        id: buttonScale
                        source: "img/zoom.png"
                        text: "Изменить масштаб"
                        opacity: containerOption.opacity
                        onClicked: {
                            menuTab.buttonScaleClicked();
                        }
                    }
                }

//                Separator{
//                    height: parent.height
//                }

//                Column{
//                    spacing: 4
//                    CheckBoxText{
//                        text: "Автоматически скрывать меню"
//                        opacity: containerOption.opacity
//                    }
//                }

            }
        }

        ButtonRotate{
            id: buttonUpDown
            source: "img/up.png"
            anchors.right: parent.right
            anchors.bottom: parent.bottom
            anchors.rightMargin: 8
            anchors.bottomMargin: 8

            property real tmpHeight: -1

            onClicked: {
                if(state === "firstState"){
                    container.height = tmpHeight
                    menuTab.height += tmpHeight
                    if(menuTab.state === "activeHome"){
                        containerHome.opacity = 1
                    } else {
                        containerOption.opacity = 1
                    }
                } else {
                    tmpHeight = container.height
                    menuTab.height -= tmpHeight
                    container.height = 0
                    containerHome.opacity = containerOption.opacity = 0
                }
            }
        }
    }

    state: "activeHome"
    states:[
        State{
            name: "activeHome";
            PropertyChanges{
                target: homeBar
                color: "white"
            }
            PropertyChanges{
                target: containerHome
                opacity:1
            }
            PropertyChanges {
                target: homeBar
                scale: 1
            }

            PropertyChanges{
                target: optionBar
                color: "lightgray"
            }
            PropertyChanges{
                target: containerOption
                opacity:0
            }
            PropertyChanges {
                target: optionBar
                scale: 0.9
            }

            PropertyChanges {
                target: menuTab
                height: {
                    return titleBar.height + titleBar.y + 4 + (buttonUpDown.tmpHeight == -1 ? container.height :  buttonUpDown.tmpHeight)
                }

            }
            PropertyChanges {
                target: container
                height: (buttonUpDown.state === "firstState" ? menuTab.height - container.y : buttonUpDown.tmpHeight)
            }
            PropertyChanges{
                target: buttonUpDown
                state: "firstState"
            }
        },
        State{
            name: "activeOption";
            PropertyChanges{
                target: homeBar
                color: "lightgray"
            }
            PropertyChanges{
                target: optionBar
                color: "white"
            }
            PropertyChanges {
                target: homeBar
                scale: 0.9
            }

            PropertyChanges{
                target: containerHome
                opacity:0
            }
            PropertyChanges{
                target: containerOption
                opacity:1
            }
            PropertyChanges {
                target: optionBar
                scale: 1
            }

            PropertyChanges {
                target: menuTab
                height: {
                    return titleBar.height + titleBar.y + 4 + (buttonUpDown.tmpHeight == -1 ? container.height :  buttonUpDown.tmpHeight)
                }

            }
            PropertyChanges {
                target: container
                height: (buttonUpDown.state === "firstState" ? menuTab.height - container.y : buttonUpDown.tmpHeight)
            }
            PropertyChanges{
                target: buttonUpDown
                state: "firstState"
            }
        }
    ]

}
