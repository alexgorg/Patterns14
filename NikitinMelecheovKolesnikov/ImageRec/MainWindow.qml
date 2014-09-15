import QtQuick 2.0

Rectangle{
    anchors.fill: parent
    color: "white"

    property var particleData

    MenuTab{
        id: menuTab
        height: 120

        onImageFileChanged: {
            imageWindow.imageName = menuTab.imageFile;

            particleData.particleInfo = [];
            particleData.bufParticle = undefined;
            particleList.updateParticleList(particleData.particleInfo);
        }
        onButtonAutoSelectClicked: {
            imageWindow.updateCalcParam(menuTab.tdelay,menuTab.horizontalAngle,
                                        menuTab.magicParam,changeScaleWindow.currentScale,menuTab.binarizationThreshold);
            imageWindow.autoSelectPartilce();
        }
        onButtonMagicPlusClicked: {
            imageWindow.enableMagicPlusButton();
            imageWindow.updateCalcParam(menuTab.tdelay,menuTab.horizontalAngle,
                                        menuTab.magicParam,changeScaleWindow.currentScale,menuTab.binarizationThreshold);
        }
        onMagicParamChanged: {
            imageWindow.updateCalcParam(menuTab.tdelay,menuTab.horizontalAngle,
                                        menuTab.magicParam,changeScaleWindow.currentScale,menuTab.binarizationThreshold);
        }
        onButtonScaleClicked: {
            changeScaleWindow.show();
        }
        onBinarizationThresholdChanged: {
            imageWindow.updateCalcParam(menuTab.tdelay,menuTab.horizontalAngle,
                                        menuTab.magicParam,changeScaleWindow.currentScale,menuTab.binarizationThreshold);
        }
        onHorizontalAngleChanged: {
            imageWindow.makeHorizontalLine(menuTab.horizontalAngle);
        }
        onButtonHelpClicked: {
            Qt.openUrlExternally("help/help.chm");
        }
        onButtonInfoClicked: {
            infoWindow.show();
        }
    }
    ParticleList{
        id: particleList
        sortParticleList: menuTab.sortSelect
        anchors.top: menuTab.bottom
        anchors.left: parent.left
        anchors.bottom: parent.bottom

        onButtonRemoveClicked: {
            var index = particleList.indexActiveParticle;
            particleData.removeParticle(index);
            particleList.updateParticleList(particleData.particleInfo);
            particleList.setIndexActiveParticle(index);

            if(particleData.particleInfo.length == 0){
                particleList.disableGraphButton();
            }

            imageWindow.requestPaint();
        }

        onButtonAddClicked: {
            var index = particleList.indexActiveParticle;
            particleData.addParticle(particleData.bufParticle);
            particleData.bufParticle = undefined;

            particleList.updateParticleList(particleData.particleInfo);
            particleList.setIndexActiveParticle(index);

            particleList.enableGraphButton();

            imageWindow.requestPaint();
        }

        onButtonGraphClicked: {
            graphWindow.show();
        }
    }
    ImageWindow{
        id: imageWindow
        anchors.top: menuTab.bottom
        anchors.left: particleList.right
        anchors.right: parent.right
        anchors.bottom: parent.bottom
        indexActiveParticle: particleList.indexActiveParticle
        particleData: parent.particleData

        onParticleListCreated: {
            particleList.updateParticleList(particleData.particleInfo);
            particleList.setIndexActiveParticle(0);

            particleList.enableGraphButton();
        }

        onImageLoaded: {
            menuTab.enableAutoSelectButton();
            menuTab.enableMagicPlusButton();
        }

        onParticleCreated: {
            particleList.enableAddButton();
        }
    }
}
