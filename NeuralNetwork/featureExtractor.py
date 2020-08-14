import glob
import argparse
import librosa.display
import matplotlib.pyplot as plt
import numpy as np
import time
import os 
from PIL import Image

FLAGS = None


def extract(fullPath,function) :
    wave_dir = os.path.split(fullPath)[0]
    wave_name = os.path.split(fullPath)[1]
    wave_name_noneExt = wave_name.split('.')[0]

    if os.path.getsize(fullPath)/1024 < 50 :
        print("파일크기 50kb 미만 :" + wave_name + "(은)는 잘못된 파일인것같습니다.")
        return

    #wave to jpg
    sound_clip,s = librosa.load(fullPath)

    #출력설정
    fig = plt.figure(figsize=(20,10),frameon=False) #2000x1000px
    fig.add_axes([0, 0, 1, 1])v

    #피쳐연산
    if function == "mfcc" :
        feature = librosa.feature.mfcc(y=sound_clip,sr=s, n_mfcc=80)
    elif function == "chroma_cqt" :
        feature = librosa.feature.chroma_cqt(y=sound_clip,sr=s)
    elif function == "chroma_stft" :
        feature = librosa.feature.chroma_stft(y=sound_clip,sr=s)
    elif function == "chroma_cens" :
        feature = librosa.feature.chroma_cens(y=sound_clip,sr=s)
    elif function == "melspectrogram" :
        feature = librosa.feature.melspectrogram(y=sound_clip,sr=s)
    else :
        print ('올바르지않은 function 값 입니다.')

    #이미지파일로 저장
    librosa.display.specshow(feature) #내부적으로 plt.plot호출
    fig.savefig(wave_dir + "\\" + wave_name_noneExt +"_"+function+".jpg") #jpg로 저장하려면 pillow라이브러리 설치

    #객체할당해제
    plt.close(fig)



if __name__ == '__main__':
  parser = argparse.ArgumentParser()
  parser.add_argument(
      '--wavePath',
      type=str,
      default='',
      help='Path to folders of labeled WaveFile.'
  )
  parser.add_argument(
      '--function',
      type=str,
      default='chroma_cqt',
      help='Select one of (chroma_cqt, chroma_stft, chroma_cens, mfcc, melspectrogram)'
  )
  #피쳐종류 chroma_cqt, chroma_stft, chroma_cens

FLAGS, unparsed = parser.parse_known_args()

if os.path.split(FLAGS.wavePath)[1] == "*.wav" :
    for file in os.listdir(os.path.split(FLAGS.wavePath)[0]):
        if file.endswith(".wav"):
            searched = os.path.join(os.path.split(FLAGS.wavePath)[0], file)
            extract(searched,FLAGS.function)
            print("complete")
else : 
    extract(FLAGS.wavePath,FLAGS.function)
    print("complete")