var LEFT, BOTTOM, RIGHT, TOP;
var used;
var points;
var di = [1,-1,0,0];
var dj = [0,0,1,-1];

//Получить отрезки по изображнию
//in: imageData.data - массив битов изображения RGBA, R = G = B, A = 255
//    imageData.width - ширина изображения
//    imageData.height - высота изображения
//    tdelay - время задержки
//    horizontAngle - угол горизонта в радианах
//    scale - масштаб 1 м к scale
//    binarizationParam - порог бинаризации от 0 до 100
//out: массив найденных частиц в виде структур:
//    {i,j,angle,velocity,width,height}
function getSegmentAuto(imageData,tdelay,horizontAngle,scale, binarizationParam){
//    console.log(binarizationParam);

    var _imageData = copy(imageData);
    _imageData = lightEdge(_imageData);
    _imageData =  adaptiveBinarization(_imageData, binarizationParam);

    var particleData = selectConnectedRegion(_imageData);
    return getAngleAndVelocityOfParticle(_imageData,particleData,tdelay,horizontAngle,scale);
}

//Эффект подсвеченные края
//in: imageData.data - массив битов изображения RGBA, R = G = B, A = 255
//    imageData.width - ширина изображения
//    imageData.height - высота изображения
//out: imageData в таком же формате, как и входные данные
//Портит входные данные!
function lightEdge(imageData){
    var mask = [
                [ 1, 1, 1],
                [ 1,-2, 1],
                [-1,-1,-1]
    ];
    var i,j,k,l,z,newColor;
    var imageWidth = imageData.width, imageHeight = imageData.height;
    var data = [];
    for(i = 1; i < imageHeight - 1; i++)
        for(j = 1; j < imageWidth - 1; j++){
            newColor = 0;
            for(k = 0; k < 3; k++)
                for(l = 0; l < 3; l++)
                    newColor += imageData.data[((i + k - 1) * imageWidth + (j + l - 1)) * 4] * mask[k][l];

            if(newColor < 0)
                newColor = 0;

            data[i * imageWidth + j] = newColor
        }
    for(i = 0; i < imageHeight; i++)
        for(j = 0; j < imageWidth; j++)
            for(z = 0; z < 3; z++)
                imageData.data[(i * imageWidth + j) * 4 + z] = data[i * imageWidth + j];
    return imageData;
}

//Адаптивная бинаризаци
//in: imageData.data - массив битов изображения RGBA, R = G = B, A = 255
//    imageData.width - ширина изображения
//    imageData.height - высота изображения
//    binarizationParam - порог бинаризации от 0 до 100
//out: imageData в таком же формате, как и входные данные, все биты изображения либо 255, либо 0
//Портит входные данные!
function adaptiveBinarization(imageData, binarizationParam){
    var i,j,k;
    var data = imageData.data;
    var imageWidth = imageData.width,
            imageHeight = imageData.height;
    var step = 30;
    for(i = 0; i < imageHeight; i+=step)
        for(j = 0; j < imageWidth; j+=step){
            imageData = binarizationAuto(imageData,i,j,Math.min(i+step,imageHeight), Math.min(j + step,imageWidth), binarizationParam);
//            console.log(i + " " + j);
        }

    var newi, newj, cntBlack,cntUsed;
    for(i = 0; i < imageHeight; i++)
        for(j = 0; j < imageWidth; j++){
            if(data[(i * imageWidth + j) * 4] === 255){
                cntBlack = 0;
                cntUsed = 0;
                for(k = 0; k < 4; k++){
                    newi = i + di[k];
                    newj = j + dj[k];
                    if(newi >= 0 && newi < imageHeight && newj >= 0 && newj < imageWidth){
                        if(data[(newi * imageWidth + newj) * 4] === 0){
                            cntBlack++;
                        }
                        cntUsed++;
                    }
                }
                if(cntBlack >=  cntUsed-1){
                    for(k = 0; k < 3; k++)
                        data[(i * imageWidth + j) * 4 + k] = 0
                }
            }
        }

    return imageData;
}

function binarizationAuto(imageData, i0,j0,i1,j1, binarizationParam){
    var i,j,k;
    var data = imageData.data;
    var imageWidth = imageData.width,
            imageHeight = imageData.height;
    var T = 0; // начальное приближение
    for(i = i0; i < i1; i++)
        for(j = j0; j < j1; j++)
            T += data[(i * imageWidth + j) * 4];
    T /= (i1 - i0) * (j1 - j0);
    var EPS = 0.00001;
    var _T = 100;
    var mObject, mBack, cntPixelObject, cntPixelBack;
    var w,h,t;
    while(Math.abs(T - _T) > EPS){
        mObject = 0; mBack = 0;
        cntPixelObject = 0;
        cntPixelBack = 0;
        for(h = i0; h < i1; h++)
            for(w = j0; w < j1; w++){
                t = data[(h * imageWidth + w) * 4];
                if(t >= T){
                    mObject += t;
                    cntPixelObject++;
                } else {
                    mBack += t;
                    cntPixelBack++;
                }
            }
        mObject /= cntPixelObject;
        mBack /= cntPixelBack;
        _T = T;
        T = (mObject + mBack)/2 + 0.015 * (binarizationParam / 50.0) / (mObject + mBack);
    }
//    console.log(binarizationParam);
    return binarization(imageData,T,i0,j0,i1,j1);
}

function binarization(imageData, middleColor,i0,j0,i1,j1){
    var i,j,k;
    var data = imageData.data;
    var imageWidth = imageData.width,
            imageHeight = imageData.height;
    for(i = i0; i < i1; i++)
        for(j = j0; j < j1; j++){
            var newColor;
            if(data[(i * imageWidth + j) * 4] > middleColor)
                newColor = 255;
            else
                newColor = 0;
            for(k = 0; k < 3; k++)
                data[(i * imageWidth + j) * 4 + k] = newColor;
        }
    return imageData;
}

//Выделение связных областей
//in: imageData.data - массив битов изображения RGBA, R = G = B, A = 255, изображение бинарное!
//    imageData.width - ширина изображения
//    imageData.height - высота изображения
//out: массив связных областей в формате:
//    {i,j,width,height}
//Производит отсеивание частиц по площади прямоугольников в котором они находятся
function selectConnectedRegion(imageData){
    var data = imageData.data;
    var imageWidth = imageData.width,
            imageHeight = imageData.height;
    var i,j,k,z;
    used = [];
    for(i = 0; i < imageHeight; i++){
        used[i] = [];
        for(j = 0; j < imageWidth; j++){
            used[i][j] = 0;
        }
    }
    var particleData = [];
    for(i = 0; i < imageHeight; i++)
        for(j = 0; j < imageWidth; j++){
            if(data[(i*imageWidth + j) * 4] === 255 && used[i][j] === 0){
                LEFT = RIGHT = j;
                BOTTOM = TOP = i;
                points = [];
                dfs(i,j,imageWidth,imageHeight,data);
                if(Math.abs(LEFT - RIGHT) * Math.abs(BOTTOM - TOP) > 500){
                    particleData.push({i:TOP, j: LEFT,width:RIGHT - LEFT, height: BOTTOM - TOP});
                } else {
                    for(k = 0; k < points.length; k++)
                        for(z = 0; z < 3; z++)
                            data[(points[k].i * imageWidth + points[k].j)*4 + z] = 0;
                }
            }
        }
    return particleData;
}
function dfs(i,j,w,h,data){
    used[i][j] = true;
    points.push({i:i,j:j});

    LEFT = Math.min(LEFT,j);
    BOTTOM = Math.max(BOTTOM,i);

    RIGHT = Math.max(RIGHT,j);
    TOP = Math.min(TOP,i);

    var k, newi, newj;
    for(k = 0; k < 4; k++){
        newi = i + di[k];
        newj = j + dj[k];
        if(newi >= 0 && newi < h && newj >= 0 && newj < w
                && used[newi][newj] === 0 && data[(newi * w + newj) * 4] === 255){
            dfs(newi,newj,w,h,data);
        }
    }
}

//Получить массив частиц с их скоростями и углами отклонения
//in: imageData.data - массив битов изображения RGBA, R = G = B, A = 255, изображение бинарное!
//    imageData.width - ширина изображения
//    imageData.height - высота изображения
//    particleData - массив связных областей в формате:
//        {i,j,width,height}
//    tdelay - время задержки в мс
//    horizontAngle - угол горизонта в радианах
//    scale - масштаб 1 м к scale
//out: массив найденных частиц в виде структур:
//    {i,j,angle,velocity,width,height}, скорость в м/с
function getAngleAndVelocityOfParticle(imageData,particleData,tdelay,horizontAngle,scale){
    var data = imageData.data;
    var imageWidth = imageData.width,
            imageHeight = imageData.height;
    var i,j,k,w,h,ang,velocity,signPlus,signMinus,_i,_j,color;
    var particleInform = [];
    for(k = 0; k < particleData.length; k++){
        i = particleData[k].i;
        j = particleData[k].j;
        w = particleData[k].width;
        h = particleData[k].height;
        ang = Math.atan2(h,w);
        signPlus = signMinus = 0;
        for(_i = i; _i <= i+h; _i++)
            for(_j = j; _j <= j+w; _j++){
                color = data[(_i * imageWidth + _j)*4];
                if(color === 255){
                    signMinus += Math.abs(h/w * (_j - j) + i - _i);
                    signPlus += Math.abs(-h/w * (_j - j) + i + h - _i);
                }
            }
        if(signMinus < signPlus)
            ang *= -1;
        ang += horizontAngle;
        velocity = Math.sqrt(w * w + h * h)/(tdelay/1000) * scale;
        particleInform.push({
                            i: i,
                            j: j,
                            angle: ang,
                            velocity: velocity,
                            width: w,
                            height: h
                            });
    }
    return particleInform;
}

//Копировать данные изображения
//in: imageData.data - массив битов изображения RGBA, R = G = B, A = 255
//    imageData.width - ширина изображения
//    imageData.height - высота изображения
//out: imageData.data - массив битов изображения RGBA, R = G = B, A = 255
//    imageData.width - ширина изображения
//    imageData.height - высота изображения
function copy(imageData){
    var copyImageData = [];
    copyImageData.data = [];
    copyImageData.width = 0;
    copyImageData.height = 0;
    for(var i = 0; i < imageData.data.length; i++)
        copyImageData.data.push(imageData.data[i]);
    copyImageData.width = imageData.width;
    copyImageData.height = imageData.height;
    return copyImageData;
}

//in: imageData.data - массив битов изображения RGBA, R = G = B, A = 255, изображение бинарное!
//    imageData.width - ширина изображения
//    imageData.height - высота изображения
//    i,j - начальное приближение
//    tdelay - время задержки в мс
//    horizontAngle - угол горизонта в радианах
//    magicParam - чувствительность волшебной палочки от 0 до 100
//    scale - масштаб 1 м к scale
//out: найденная частица в виде структуры:
//    {i,j,angle,velocity,width,height}, скорость в м/с
function magic(imageData,i,j,tdelay,horizontAngle,magicParam,scale){
    var imageWidth = imageData.width,
            imageHeight = imageData.height,
            data = imageData.data;
    var used = [];
    i = Math.floor(i);
    j = Math.floor(j);
    var totalColor = data[(i * imageWidth + j) * 4];
    var cntPoints = 1;
    LEFT = RIGHT = j;
    TOP = BOTTOM = i;
    var q = [];
    var h = 0,t = 0;
    var ci,cj,ni,nj,k,pixelColor,ro;
    for(ci = 0; ci < imageHeight; ci++){
        used[ci] = [];
        for(cj = 0; cj < imageWidth; cj++)
            used[ci][cj] = false;
    }
    q[h++] = [i,j];
    used[i][j] = true;
    while(t < h){
        ci = q[t][0];
        cj = q[t][1];
        t++;
        for(k = 0; k < 4; k++){
            ni = di[k] + ci;
            nj = dj[k] + cj;
            if(ni >= 0 && ni < imageHeight && nj >= 0 && nj < imageWidth && !used[ni][nj]){
                pixelColor = data[4*(ni * imageWidth + nj)];
                ro = Math.sqrt((pixelColor - totalColor) * (pixelColor - totalColor))
                if(ro < 255.0 * magicParam / 100.0){
                    LEFT = Math.min(LEFT,nj);
                    BOTTOM = Math.max(BOTTOM,ni);

                    RIGHT = Math.max(RIGHT,nj);
                    TOP = Math.min(TOP,ni);

                    used[ni][nj] = true;
                    q[h++] = [ni,nj];
                }
            }
        }
    }
    var res = {i:TOP,j:LEFT,width:RIGHT - LEFT,height:BOTTOM - TOP,angle:0,velocity:0};
    res.velocity = Math.sqrt(res.width * res.width + res.height * res.height)/(tdelay/1000) * scale;
    res.angle = Math.atan2(res.height,res.width);
    var signPlus = 0, signMinus = 0;
    var _i,_j;
    for(_i = res.i; _i <= res.i+res.height; _i++)
        for(_j = res.j; _j <= res.j+res.width; _j++){
            if(used[_i][_j]){
                signMinus += Math.abs(res.height/res.width * (_j - res.j) + res.i - _i);
                signPlus += Math.abs(-res.height/res.width * (_j - res.j) + res.i + res.height - _i);
            }
        }
    if(signMinus < signPlus)
        res.angle *= -1;
    res.angle += horizontAngle;
    return res;
}
