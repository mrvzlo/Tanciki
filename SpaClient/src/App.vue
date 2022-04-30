<style lang="scss">
@import '@/assets/main.scss';
</style>

<template>
   <game :id="id" />
</template>

<script lang="ts">
import { Options, Vue } from 'vue-class-component';
import Game from './components/Game.vue';
import PlayerCookieService from '@/data-access/cookies/player-cookie.service';

@Options({
   components: {
      Game,
   },
})
export default class App extends Vue {
   cookieService = new PlayerCookieService();
   id = this.cookieService.getId();
}
</script>
